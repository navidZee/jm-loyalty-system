using JMLS.Domain.InteractionTypes;

namespace JMLS.Tests.UnitTests.Domain.InteractionTypes;

public class InteractionTypeTestDataBuilder
{
    private string _key = "TEST_KEY";
    private string _description = "Test Description";
    private decimal _amount = 100.0m;
    private TimeSpan? _expirationPeriod = TimeSpan.FromDays(30);

    public InteractionTypeTestDataBuilder WithKey(string key)
    {
        _key = key;
        return this;
    }

    public InteractionTypeTestDataBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public InteractionTypeTestDataBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public InteractionTypeTestDataBuilder WithExpirationPeriod(TimeSpan? expirationPeriod)
    {
        _expirationPeriod = expirationPeriod;
        return this;
    }

    public InteractionType Build()
    {
        return new InteractionType(_key, _description, _amount, _expirationPeriod)
        {
            Key = _key,
            Description = _description
        };
    }
}