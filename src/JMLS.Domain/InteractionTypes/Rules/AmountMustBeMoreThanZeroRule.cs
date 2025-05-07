namespace JMLS.Domain.InteractionTypes.Rules;

public class AmountMustBeMoreThanZeroRule(decimal amount) : IBusinessRule
{
    public bool IsBroken()
    {
        return amount <= 0;
    }

    public string Message => "The amount must be more than zero.";
}