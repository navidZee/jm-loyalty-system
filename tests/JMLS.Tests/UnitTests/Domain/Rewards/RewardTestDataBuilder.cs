using JMLS.Domain.Rewards;

namespace JMLS.Tests.UnitTests.Domain.Rewards;

public class RewardTestDataBuilder
{
    private int _referenceId = 100;
    private RewardReferenceType _rewardReferenceType = RewardReferenceType.Catalog;
    private decimal _amount = 50.0m;

    public RewardTestDataBuilder WithReferenceId(int referenceId)
    {
        _referenceId = referenceId;
        return this;
    }

    public RewardTestDataBuilder WithRewardReferenceType(RewardReferenceType rewardReferenceType)
    {
        _rewardReferenceType = rewardReferenceType;
        return this;
    }

    public RewardTestDataBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public Reward Build()
    {
        return new Reward(_referenceId, _rewardReferenceType, _amount);
    }
}