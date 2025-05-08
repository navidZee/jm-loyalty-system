using JMLS.Domain.Activities;
using JMLS.Domain.Offers;
using JMLS.Domain.Points;
using JMLS.Domain.Points.Rules;

namespace JMLS.Domain.Customers;

public class Customer : EntityBase
{
    private Customer()
    {
    }

    public Customer(string username)
    {
        Username = username;
        DateCreated = DateTime.Now;
        DateModified = DateTime.Now;
    }

    public int Id { get; set; }
    public string Username { get; private set; }

    private readonly List<PointEarned> _pointsEarned = [];
    public IReadOnlyList<PointEarned> PointsEarned => _pointsEarned;

    private readonly List<PointSpent> _pointsSpent = [];
    public IReadOnlyList<PointSpent> PointsSpent => _pointsSpent;

    public int PointBalance => PointsEarned.Where(pe => pe.IsActive).Sum(x => x.PointValue) -
                               PointsSpent.Sum(x => x.PointValue);

    public void Earned(Activity activity, int referenceId)
    {
        CheckRules(new ActivityMustBeProvidedRule(activity), new ReferenceIdMustBeMoreThanZero(referenceId));

        _pointsEarned.Add(new PointEarned(activity, referenceId));
    }

    public void Spent(Offer offer)
    {
        CheckRules(new OfferMustBeProvidedRule(offer),
            new BalanceMustBeGreaterThanPointSpentCostRule(PointBalance, offer));

        _pointsSpent.Add(new PointSpent(offer));
    }
}