using JMLS.Domain.Rewards;

namespace JMLS.Domain.Points;

public class PointsSpent : PointTransaction
{
    public int RewardId { get; set; }
    public required Reward Reward { get; set; }
}