using JMLS.Domain.Activities;
using JMLS.Domain.Customers;
using JMLS.Domain.Offers;
using JMLS.Domain.Points.Rules;

namespace JMLS.Domain.Points;

public class Point : EntityBase
{
    private Point()
    {
    }

    public Point(int customerId)
    {
        CheckRules(new CustomerIdMustBeMoreThanZeroRule(customerId));

        CustomerId = customerId;
        Balance = 0;
        DateCreated = DateTime.Now;
        DateModified = DateTime.Now;
    }

    public int Id { get;  set; }
    public int CustomerId { get; private set; }
    public int Balance { get; private set; }
    public Customer Customer { get; set; } = null!;

    private readonly List<PointEarned> _pointsEarned = [];
    public IReadOnlyList<PointEarned> PointsEarned => _pointsEarned;

    private readonly List<PointSpent> _pointsSpent = [];
    public IReadOnlyList<PointSpent> PointsSpent => _pointsSpent;

    private void SetBalance()
    {
        Balance = PointsEarned.Where(pe => pe.IsActive).Sum(x => x.PointValue) - PointsSpent.Sum(x => x.PointValue);
        DateModified = DateTime.Now;
    }

    public void Earned(Activity activity, int referenceId)
    {
        CheckRules(new ActivityMustBeProvidedRule(activity), new ReferenceIdMustBeMoreThanZero(referenceId));

        _pointsEarned.Add(new PointEarned(activity, referenceId));
        SetBalance();
    }

    public void Spent(Offer offer)
    {
        CheckRules(new OfferMustBeProvidedRule(offer),
            new BalanceMustBeGreaterThanPointSpentCostRule(Balance, offer));

        _pointsSpent.Add(new PointSpent(offer));
        SetBalance();
    }
}