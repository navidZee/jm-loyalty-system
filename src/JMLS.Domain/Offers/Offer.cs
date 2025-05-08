using JMLS.Domain.Offers.Rules;
using JMLS.Domain.Points;

namespace JMLS.Domain.Offers;

public class Offer : EntityBase
{
    private Offer()
    {
    }

    public Offer(int pointsCost, OfferType type, decimal amount, int referenceId, OfferReferenceType referenceType)
    {
        CheckRules(
            new OfferAmountMustBeMoreThanZeroRule(amount),
            new OfferReferenceIdMustBeProvidedRule(referenceId),
            new OfferPointsCostMustBeMoreThanZeroRule(pointsCost));

        Code = Guid.NewGuid().ToString();
        PointsCost = pointsCost;
        Type = type;
        Amount = amount;
        ReferenceId = referenceId;
        ReferenceType = referenceType;
        DateCreated = DateTime.Now;
        DateModified = DateTime.Now;
    }

    public int Id { get; set; }
    public string Code { get; private set; }
    public int PointsCost { get; private set; }
    public OfferType Type { get; private set; }
    public decimal Amount { get; private set; }
    public int ReferenceId { get; private set; }
    public OfferReferenceType ReferenceType { get; private set; }
    public required IReadOnlyList<PointSpent> PointsSpent { get; set; }
}

public enum OfferType
{
    Flat,
    Percentage
}

public enum OfferReferenceType
{
    Catalog,
    Discount
}