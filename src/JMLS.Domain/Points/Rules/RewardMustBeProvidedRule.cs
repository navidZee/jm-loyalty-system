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

public class BalanceMustBeGreaterThanPointSpentCostRule(int balance, Offer offer) : IBusinessRule
{
    public bool IsBroken()
    {
        return balance < offer.PointsCost;
    }

    public string Message => "Insufficient point balance for this offer";
}