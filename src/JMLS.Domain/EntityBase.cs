namespace JMLS.Domain;

public abstract class EntityBase
{
    public DateTime DateCreated { get; protected set; }
    public DateTime DateModified { get; protected set; }

    protected static void CheckRules(params IBusinessRule[] rules)
    {
        foreach (var rule in rules)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}
