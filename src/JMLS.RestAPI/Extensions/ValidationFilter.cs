using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace JMLS.RestAPI.Extensions;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
    private readonly IValidator<T> _validator;

    public ValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var parameter = context.Arguments.OfType<T>().FirstOrDefault();
        if (parameter is null)
        {
            return Results.BadRequest("Invalid request payload");
        }

        var validationResult = await _validator.ValidateAsync(parameter);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        return await next(context);
    }
}