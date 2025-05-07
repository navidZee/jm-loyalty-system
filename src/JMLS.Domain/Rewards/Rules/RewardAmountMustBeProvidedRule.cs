namespace JMLS.Domain.Rewards.Rules;

public class RewardAmountMustBeMoreThanZeroRule(decimal amount) : IBusinessRule
{
    public bool IsBroken()
    {
        return amount <= 0;
    }

    public string Message => "Amount must be greater than 0";
}