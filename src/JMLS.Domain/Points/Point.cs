using JMLS.Domain.Customers;
using JMLS.Domain.InteractionTypes;
using JMLS.Domain.Points.Rules;
using JMLS.Domain.Rewards;

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
        DateModified = DateTime.Now;
        DateModified = DateTime.Now;
    }

    public int Id { get; set; }
    public int CustomerId { get; private set; }
    public decimal Balance { get; private set; }
    
    public required Customer Customer { get; set; }
    public required List<PointEarned> PointsEarned { get; set; }
    public required List<PointSpent> PointsSpent { get; set; }

    private void SetBalance()
    {
        Balance = PointsEarned.Where(pe => pe.IsActive).Sum(x => x.Amount) - PointsSpent.Sum(x => x.Amount);
        DateModified = DateTime.Now;
    }

    public void Earned(InteractionType interactionType)
    {
        CheckRules(new InteractionTypeMustBeProvidedRule(interactionType));

        PointsEarned.Add(new PointEarned(interactionType));
        SetBalance();
    }

    public void Spent(Reward reward)
    {
        CheckRules(new RewardMustBeProvidedRule(reward));

        PointsSpent.Add(new PointSpent(reward));
        SetBalance();
    }
}