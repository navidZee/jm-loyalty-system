using JMLS.Domain.Customers;

namespace JMLS.Tests.UnitTests.Domain.Customers;

public class CustomerTestDataBuilder
{
    private string _username = "testuser";

    public CustomerTestDataBuilder WithUsername(string username)
    {
        _username = username;
        return this;
    }

    public Customer Build()
    {
        return new Customer(_username);
    }
}