using JMLS.Domain.Offers;

namespace JMLS.Domain.Points.Rules;

public class OfferMustBeProvidedRule(Offer offer) : IBusinessRule
{
    public bool IsBroken()
    {
        return offer == null!;
    }

    public string Message => "Offer must be provided";
}