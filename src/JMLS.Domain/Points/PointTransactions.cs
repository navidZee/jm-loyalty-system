namespace JMLS.Domain.Points;

public abstract class PointTransaction  : EntityBase
{
    public int Id { get; set; }
    public int PointId { get; set; }
    public int PointsValue { get; protected init; }
    public  Point Point { get; set; } = null!;
}