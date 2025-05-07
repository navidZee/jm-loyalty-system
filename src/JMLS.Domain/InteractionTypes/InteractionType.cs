using JMLS.Domain.InteractionTypes.Rules;

namespace JMLS.Domain.InteractionTypes;

public class InteractionType : EntityBase
{
    public int Id { get; set; }

    private InteractionType()
    {
    }

    public InteractionType(string key, string description, decimal amount, TimeSpan? expirationPeriod)
    {
        CheckRules(
            new KeyMustBeProvidedRule(key),
            new AmountMustBeMoreThanZeroRule(amount),
            new DescriptionMustBeProvidedRule(description));

        Key = key;
        Description = description;
        Amount = amount;
        ExpirationPeriod = expirationPeriod;
        DateModified = DateTime.Now;
        DateCreated = DateTime.Now;
    }

    public required string Key { get; init; }
    public required string Description { get; init; }
    public decimal Amount { get; init; }
    public TimeSpan? ExpirationPeriod { get; init; }
}