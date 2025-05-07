namespace JMLS.Domain.Points;

public abstract class PointTransaction
{
    public int Id { get; set; }
    public int PointId { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }

    public required Point Point { get; set; }
}