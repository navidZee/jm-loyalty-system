using JMLS.Domain.Offers.Rules;

namespace JMLS.Domain.Offers;

public class Offer : EntityBase
{
    private Offer()
    {
    }

    public Offer(int pointSpent, OfferType type, decimal amount, int referenceId, OfferReferenceType referenceType)
    {
        CheckRules(
            new OfferAmountMustBeMoreThanZeroRule(amount),
            new OfferReferenceIdMustBeProvidedRule(referenceId),
            new OfferPointSpentMustBeMoreThanZeroRule(pointSpent));

        PointSpent = pointSpent;
        Type = type;
        Amount = amount;
        ReferenceId = referenceId;
        ReferenceType = referenceType;
        Code = Guid.NewGuid().ToString();
        DateCreated = DateTime.Now;
        DateModified = DateTime.Now;
    }

    public int Id { get; set; }
    public int PointSpent { get; private set; }
    public OfferType Type { get; private set; }
    public decimal Amount { get; private set; }
    public int ReferenceId { get; private set; }
    public OfferReferenceType ReferenceType { get; private set; }
    public string Code { get; private set; }
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