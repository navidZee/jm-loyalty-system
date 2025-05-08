using FluentValidation;
using JMLS.RestAPI.Requests;

namespace JMLS.RestAPI.Validators;

public class RecordCustomerPointEarnedValidator : AbstractValidator<RequestToEarnDto>
{
    public RecordCustomerPointEarnedValidator()
    {
        _ = this.RuleFor(r => r.ActivityId).NotEqual(0).WithMessage("activity id cannot be 0");
        _ = this.RuleFor(r => r.ReferenceId).NotEqual(0).WithMessage("activity reference id cannot be 0");
    }
}
