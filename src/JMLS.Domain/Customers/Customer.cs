using JMLS.Domain.Points;

namespace JMLS.Domain.Customers;

public class Customer : EntityBase
{
    private Customer()
    {
    }

    public Customer(string username)
    {
        Username = username;
        DateCreated = DateTime.Now;
        DateModified = DateTime.Now;
    }

    public int Id { get; set; }
    public string Username { get; private set; }
    public Point? Point { get;  set; }
}