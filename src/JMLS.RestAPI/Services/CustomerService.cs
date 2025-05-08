using JMLS.Domain.Customers;
using JMLS.RestAPI.Infrastructure.Persistence.SQL;
using Microsoft.EntityFrameworkCore;

namespace JMLS.RestAPI.Services;

public interface ICustomerService
{
    Task<Customer?> GetByUserNameAsync(string keycloakId);
    Task<Customer> CreateAsync(string username);
}

public class CustomerService(JmlsDbContext dbContext) : ICustomerService
{
    public async Task<Customer?> GetByUserNameAsync(string username)
    {
        return await dbContext.Customers
            .FirstOrDefaultAsync(c => c.Username == username);
    }

    public async Task<Customer> CreateAsync(string username)
    {
        var customer = new Customer(username);
        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync();
        return customer;
    }
}