using JMLS.Domain.Rewards;

namespace JMLS.Domain.Points.Rules;

public class RewardMustBeProvidedRule(Reward reward) : IBusinessRule
{
    public bool IsBroken()
    {
        return reward is { Id: <= 0 };
    }

    public string Message => "Reward must be provided";
}