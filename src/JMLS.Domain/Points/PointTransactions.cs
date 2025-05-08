using JMLS.Domain.Customers;

namespace JMLS.Domain.Points;

public abstract class PointTransaction : EntityBase
{
    public int Id { get; set; }
    public int PointValue { get; protected init; }
    public int CustomerId { get; private set; }
    public Customer Customer { get; set; } = null!;
}