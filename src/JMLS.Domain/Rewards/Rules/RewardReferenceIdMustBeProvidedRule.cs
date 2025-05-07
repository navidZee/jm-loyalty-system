namespace JMLS.Domain.Rewards.Rules;

public class RewardReferenceIdMustBeProvidedRule : IBusinessRule
{
    private readonly int _referenceId;
    
    public RewardReferenceIdMustBeProvidedRule(int referenceId)
    {
        _referenceId = referenceId;
    }
    
    public bool IsBroken()
    {
        return _referenceId <= 0;
    }

    public string Message => "ReferenceId must be provided.";
}