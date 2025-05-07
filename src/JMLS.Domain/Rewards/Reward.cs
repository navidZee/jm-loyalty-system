using JMLS.Domain.Rewards.Rules;

namespace JMLS.Domain.Rewards;

public class Reward : EntityBase
{
    private Reward()
    {
    }

    public Reward(int rewardReferenceId, RewardReferenceType rewardReferenceType, decimal amount)
    {
        CheckRules(new RewardReferenceIdMustBeProvidedRule(rewardReferenceId),
                   new RewardAmountMustBeMoreThanZeroRule(amount));

        RewardReferenceId = rewardReferenceId;
        RewardReferenceType = rewardReferenceType;
        Amount = amount;
        DateCreated = DateTime.Now;
        DateModified = DateTime.Now;
    }

    public int Id { get; set; }
    public int RewardReferenceId { get; set; }
    public RewardReferenceType RewardReferenceType { get; set; }
    public decimal Amount { get; set; }
}

public enum RewardReferenceType
{
    Catalog,
    Discount
}