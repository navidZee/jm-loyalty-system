using JMLS.Domain.Customers;
using JMLS.Domain.Points;

namespace JMLS.Tests.UnitTests.Domain.Points;

public class PointTestDataBuilder
{
    private int _customerId = 100;

    public PointTestDataBuilder WithCustomerId(int customerId)
    {
        _customerId = customerId;
        return this;
    }

    public Point Build()
    {
        var point = new Point(_customerId)
        {
            Customer = new Customer() { Id = _customerId }
        };
        return point;
    }
}