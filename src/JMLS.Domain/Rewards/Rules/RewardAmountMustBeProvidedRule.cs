namespace JMLS.Domain.Rewards.Rules;

public class RewardAmountMustBeMoreThanZeroRule : IBusinessRule
{
    private readonly decimal _amount;
    
    public RewardAmountMustBeMoreThanZeroRule(decimal amount)
    {
        _amount = amount;
    }
    
    public bool IsBroken()
    {
        return _amount <= 0;
    }

    public string Message => "Amount must be greater than 0";
}