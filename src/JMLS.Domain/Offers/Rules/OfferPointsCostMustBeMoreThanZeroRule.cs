namespace JMLS.Domain.Offers.Rules;

public class OfferPointsCostMustBeMoreThanZeroRule(int pointsCost) : IBusinessRule
{
    public bool IsBroken()
    {
        return pointsCost <= 0;
    }

    public string Message => "Points cost must be greater than 0";
}