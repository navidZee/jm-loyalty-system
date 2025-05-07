namespace JMLS.Domain.Points.Rules;

public class CustomerIdMustBeMoreThanZeroRule(int customerId) : IBusinessRule
{
    public bool IsBroken()
    {
        return customerId <= 0;
    }

    public string Message => "Customer Id must be more than zero.";
}