namespace JMLS.Domain.Offers.Rules;

public class OfferPointSpentMustBeMoreThanZeroRule(int pointSpent) : IBusinessRule
{
    public bool IsBroken()
    {
        return pointSpent <= 0;
    }

    public string Message => "Point spent must be greater than 0";
}