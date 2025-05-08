using JMLS.Domain.Customers;
using JMLS.RestAPI.Infrastructure.Persistence.SQL;
using JMLS.RestAPI.Requests;
using Microsoft.EntityFrameworkCore;

namespace JMLS.RestAPI.Services;

public interface ICustomerService
{
    Task<Customer?> GetByUserNameAsync(string keycloakId);
    Task<Customer> CreateAsync(string username);
    public Task RequestToEarn(RequestToEarnDto requestToEarnDto, CancellationToken cancellationToken);
    public Task RequestToSpent(RequestToSpentDto requestToSpentDto, CancellationToken cancellationToken);
    public Task<int> GetPointsBalance(int customerId, CancellationToken cancellationToken);
}

public class CustomerService(JmlsDbContext jmlsDbContext) : ICustomerService
{
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
    
    public async Task RequestToEarn(RequestToEarnDto requestToEarnDto,
        CancellationToken cancellationToken)
    {
        var customer = await jmlsDbContext.Customers
            .Include(d => d.PointsEarned)
            .Include(d => d.PointsSpent)
            .FirstAsync(d => d.Id == requestToEarnDto.CustomerId, cancellationToken);

        var activity = await jmlsDbContext.Activities.FirstOrDefaultAsync(d => d.Id == requestToEarnDto.ActivityId,
                cancellationToken);

        customer.Earned(activity!, requestToEarnDto.ReferenceId);
        jmlsDbContext.Customers.Update(customer);
        await jmlsDbContext.SaveChangesAsync(cancellationToken);
    }   
    
    public async Task RequestToSpent(RequestToSpentDto requestToEarnDto,
        CancellationToken cancellationToken)
    {
        var customer = await jmlsDbContext.Customers
            .Include(d => d.PointsEarned)
            .Include(d => d.PointsSpent)
            .FirstAsync(d => d.Id == requestToEarnDto.CustomerId, cancellationToken);
        
        var offer = await jmlsDbContext.Offers.FirstOrDefaultAsync(d => d.Id == requestToEarnDto.OfferId, cancellationToken);

        customer.Spent(offer!);
        jmlsDbContext.Customers.Update(customer);
        await jmlsDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetPointsBalance(int customerId, CancellationToken cancellationToken)
    {
        var customer = await jmlsDbContext.Customers
            .Include(d => d.PointsEarned)
            .Include(d => d.PointsSpent)
            .FirstAsync(d => d.Id == customerId, cancellationToken);
        
        return customer.PointBalance;
    }
}