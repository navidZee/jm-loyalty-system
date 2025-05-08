using JMLS.Domain.Points;

namespace JMLS.Domain.Customers;

public class Customer
{
    public int Id { get; set; }
    
    public Point? Point {get; set;}
}