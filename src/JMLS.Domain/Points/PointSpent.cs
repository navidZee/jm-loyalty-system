using JMLS.Domain.Offers;

namespace JMLS.Domain.Points;

public class PointSpent : PointTransaction
{
    private PointSpent()
    {
    }

    internal PointSpent(Offer offer)
    {
        OfferId = offer.Id;
        PointValue = offer.PointsCost;
        DateCreated = DateTime.Now;
        DateModified = DateTime.Now;
    }

    public int OfferId { get; set; }
    public Offer Offer { get; set; } = null!;
}