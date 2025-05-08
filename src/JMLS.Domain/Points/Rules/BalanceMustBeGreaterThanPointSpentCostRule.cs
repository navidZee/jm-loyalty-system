using JMLS.Domain.Offers;

namespace JMLS.Domain.Points.Rules;

public class BalanceMustBeGreaterThanPointSpentCostRule(int balance, Offer offer) : IBusinessRule
{
    public bool IsBroken()
    {
        return balance < offer.PointsCost;
    }

    public string Message => "Insufficient point balance for this offer";
}