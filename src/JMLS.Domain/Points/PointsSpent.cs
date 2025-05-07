using JMLS.Domain.Rewards;

namespace JMLS.Domain.Points;

public class PointSpent : PointTransaction
{
    private PointSpent()
    {
    }

    public PointSpent(Reward reward)
    {
        
        RewardId = reward.Id;
        Amount = reward.Amount;
        DateCreated = DateTime.Now;
        DateModified = DateTime.Now;
    }

    public int RewardId { get; set; }
    public Reward Reward { get; set; } = null!;
}