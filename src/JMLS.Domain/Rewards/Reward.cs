namespace JMLS.Domain.Rewards;

public class Reward
{
    public int Id { get; set; }
    public int ReferenceId { get; set; }
    public RewardType RewardType { get; set; }
    public decimal PointsCost { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public enum RewardType
{
    Catalog,
    Discount
}