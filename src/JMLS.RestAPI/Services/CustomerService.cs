using JMLS.Domain.Customers;
using JMLS.RestAPI.Infrastructure.Persistence.SQL;
using JMLS.RestAPI.Requests;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace JMLS.RestAPI.Services;

public interface ICustomerService
{
    Task<Customer?> GetByUserNameAsync(string keycloakId);
    Task<Customer> CreateAsync(string username);
    public Task RequestToPointTransaction(RequestToPointTransactionContextDto dto,
        CancellationToken cancellationToken);
    public Task<int> GetPointsBalance(int customerId, CancellationToken cancellationToken);
}

public abstract class CustomerService(JmlsDbContext jmlsDbContext, IConnectionMultiplexer connectionMultiplexer)
    : ICustomerService
{
    const string POINT_CUSTOMER_KEY = "points:customers:{0}";
    const string POINT_BALANCE_KEY = "points:balance:{0}";

    public async Task<Customer?> GetByUserNameAsync(string username)
    {
        return await jmlsDbContext.Customers.FirstOrDefaultAsync(c => c.Username == username);
    }

    public async Task<Customer> CreateAsync(string username)
    {
        var customer = new Customer(username);
        jmlsDbContext.Customers.Add(customer);
        await jmlsDbContext.SaveChangesAsync();
        return customer;
    }

    protected abstract Task<Customer> RequestToRequestToPointTransaction(
        RequestToPointTransactionContextDto requestToPointTransactionContextDto,
        CancellationToken cancellationToken);

    public async Task RequestToPointTransaction(RequestToPointTransactionContextDto dto,
        CancellationToken cancellationToken)
    {
        var lockKey = string.Format(POINT_CUSTOMER_KEY, dto.CustomerId);
        var localValue = "Lock";

        try
        {
            var lockIsTaken = await connectionMultiplexer.GetDatabase()
                .LockTakeAsync(lockKey, "Lock", TimeSpan.FromSeconds(1));

            if (!lockIsTaken)
            {
                throw new ApplicationException("Points already locked");
            }

            dto.Customer = await jmlsDbContext.Customers
                .Include(d => d.PointsEarned)
                .Include(d => d.PointsSpent)
                .FirstAsync(d => d.Id == dto.CustomerId, cancellationToken);

            var customer = await RequestToRequestToPointTransaction(dto, cancellationToken);

            jmlsDbContext.Customers.Update(customer);
            await jmlsDbContext.SaveChangesAsync(cancellationToken);

            var balance = customer.PointBalance;
            await UpdatePointBalanceCache(customer.Id, balance);
        }
        finally
        {
            await connectionMultiplexer.GetDatabase().LockReleaseAsync(lockKey, localValue);
        }
    }

    public async Task<int> GetPointsBalance(int customerId, CancellationToken cancellationToken)
    {
        var key = string.Format(POINT_BALANCE_KEY, customerId);
        var db = connectionMultiplexer.GetDatabase();

        var cachedBalance = await db.StringGetAsync(key);
        if (cachedBalance.HasValue)
        {
            return (int)cachedBalance;
        }

        var customer = await jmlsDbContext.Customers
            .Include(d => d.PointsEarned)
            .Include(d => d.PointsSpent)
            .FirstAsync(d => d.Id == customerId, cancellationToken);

        var balance = customer.PointBalance;
        await UpdatePointBalanceCache(customerId, balance);

        return balance;
    }

    private async Task UpdatePointBalanceCache(int customerId, int balance)
    {
        var key = string.Format(POINT_BALANCE_KEY, customerId);
        var db = connectionMultiplexer.GetDatabase();

        var now = DateTime.Now;
        var endOfDay = now.Date.AddDays(1).AddSeconds(-1);
        var expirationTime = endOfDay - now;

        await db.StringSetAsync(key, balance, expirationTime);
    }
}