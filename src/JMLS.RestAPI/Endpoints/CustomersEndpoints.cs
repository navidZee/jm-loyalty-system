using JMLS.Domain;
using JMLS.RestAPI.Extensions;
using JMLS.RestAPI.Requests;
using JMLS.RestAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JMLS.RestAPI.Endpoints;

public static class CustomersEndpoints
{
    public static WebApplication MapPointEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/customers")
            .WithTags("CustomerPoints")
            .WithDescription("Actions that describe the customer points")
            .WithOpenApi()
            .RequireAuthorization();

        _ = root.MapGet("/balance", GetPointsBalance)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Returns the balance")
            .Produces<int>()
            .WithDescription("Get customers balance")
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        _ = root.MapPost("/earn", RequestToEarn)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Apply customers earned")
            .WithDescription("Apply customers earned")
            .ProducesProblem(StatusCodes.Status401Unauthorized);
        
        _ = root.MapPost("/spent", RequestToSpent)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .WithSummary("Apply customers spent")
            .WithDescription("Apply customers spent")
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        return app;
    }

    private static async Task<IResult> GetPointsBalance(
        [FromServices] ICustomerService customerService,
        HttpContext context,
        CancellationToken cancellationToken)
    {
        try
        {
            var customerId = await context.User.GetOrCreateCustomerIdAsync(customerService);
            var balance = await customerService.GetPointsBalance(customerId, cancellationToken);
            return Results.Ok(balance);
        }
        catch (Exception e)
        {
            return Results.Problem(e.StackTrace, "Error", StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> RequestToEarn(
        [FromBody] RequestToEarnDto requestToEarnDto,
        [FromServices] ICustomerService customerService,
        HttpContext context,
        CancellationToken cancellationToken)
    {
        try
        {
            requestToEarnDto.CustomerId = await context.User.GetOrCreateCustomerIdAsync(customerService);
            await customerService.RequestToEarn(requestToEarnDto, cancellationToken);
            return Results.Ok();
        }
        catch (BusinessRuleValidationException ex)
        {
            return Results.BadRequest(ex.Message);
        }
        catch (Exception e)
        {
            return Results.Problem(e.StackTrace, "Error", StatusCodes.Status500InternalServerError);
        }
    }
    
    private static async Task<IResult> RequestToSpent(
        [FromBody] RequestToSpentDto requestToSpentDto,
        [FromServices] ICustomerService customerService,
        HttpContext context,
        CancellationToken cancellationToken)
    {
        try
        {
            requestToSpentDto.CustomerId = await context.User.GetOrCreateCustomerIdAsync(customerService);
            await customerService.RequestToSpent(requestToSpentDto, cancellationToken);
            return Results.Ok();
        }
        catch (BusinessRuleValidationException ex)
        {
            return Results.BadRequest(ex.Message);
        }
        catch (Exception e)
        {
            return Results.Problem(e.StackTrace, "Error", StatusCodes.Status500InternalServerError);
        }
    }
}