using JMLS.Domain.Offers;

namespace JMLS.Tests.UnitTests.Domain.Offers;

public class OfferTestDataBuilder
{
    private int _pointsCost = 100;
    private OfferType _type = OfferType.Flat;
    private decimal _amount = 50.0m;
    private int _referenceId = 1;
    private OfferReferenceType _referenceType = OfferReferenceType.Catalog;

    public OfferTestDataBuilder WithPointsCost(int pointsCost)
    {
        _pointsCost = pointsCost;
        return this;
    }

    public OfferTestDataBuilder WithType(OfferType type)
    {
        _type = type;
        return this;
    }

    public OfferTestDataBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public OfferTestDataBuilder WithReferenceId(int referenceId)
    {
        _referenceId = referenceId;
        return this;
    }

    public OfferTestDataBuilder WithReferenceType(OfferReferenceType referenceType)
    {
        _referenceType = referenceType;
        return this;
    }

    public Offer Build()
    {
        return new Offer(_pointsCost, _type, _amount, _referenceId, _referenceType)
        {
            PointsSpent = []
        };
    }
}