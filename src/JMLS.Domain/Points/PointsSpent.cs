using JMLS.Domain.Offers;

namespace JMLS.Domain.Points;

public class PointSpent : PointTransaction
{
    private PointSpent()
    {
    }

    public PointSpent(Offer offer)
    {
        OfferId = offer.Id;
        PointsValue = offer.PointSpent;
        DateCreated = DateTime.Now;
        DateModified = DateTime.Now;
    }

    public int OfferId { get; set; }
    public Offer Offer { get; set; } = null!;
}