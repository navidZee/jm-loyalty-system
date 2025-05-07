using JMLS.Domain.Customers;

namespace JMLS.Domain.Points;

public class Point
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public decimal Balance { get; set; }
    
    public required Customer Customer { get; set; }
    public required List<PointTransaction> Transactions { get; set; }
}