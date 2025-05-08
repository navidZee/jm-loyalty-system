namespace JMLS.Domain.Points.Rules;

public class ReferenceIdMustBeMoreThanZero(int referenceId) : IBusinessRule
{
    public bool IsBroken()
    {
        return referenceId <= 0;
    }

    public string Message => "Reference id must be more than zero";
}