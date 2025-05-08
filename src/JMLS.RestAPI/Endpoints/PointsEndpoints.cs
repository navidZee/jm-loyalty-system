using JMLS.Domain;
using JMLS.RestAPI.Extensions;
using JMLS.RestAPI.Requests;
using JMLS.RestAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JMLS.RestAPI.Endpoints;

public static class PointsEndpoints
{
    public static WebApplication MapPointEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/customers/{id:int}/points")
            .WithTags("CustomerPoints")
            .WithDescription("Actions that describe the customer points")
            .WithOpenApi();

        _ = root.MapGet("/balance", GetCustomerPointsBalance)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Returns the balance")
            .Produces<int>()
            .WithDescription("Get customers balance");

        _ = root.MapPost("/earn", RequestToEarn)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .WithSummary("Applay customers earned")
            .WithDescription("Applay customers earned");
        
        _ = root.MapPost("/spent", RequestToSpent)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .WithSummary("Applay customers spent")
            .WithDescription("Applay customers spent");

        return app;
    }

    private static async Task<IResult> GetCustomerPointsBalance([FromRoute] int id,
        [FromServices] IPointService pointService,
        CancellationToken cancellationToken)
    {
        try
        {
            var balance =
                await pointService.GetCustomerPointsBalance(IdentityExtensions.GetUserId(), cancellationToken);
            return Results.Ok(balance);
        }
        catch (Exception e)
        {
            return Results.Problem(e.StackTrace, "Error", StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task<IResult> RequestToEarn([FromBody] RequestToEarnDto requestToEarnDto,
        [FromRoute] int id, [FromServices] IPointService pointService,
        CancellationToken cancellationToken)
    {
        try
        {
            requestToEarnDto.CustomerId = IdentityExtensions.GetUserId();
            await pointService.RequestToEarn(requestToEarnDto, cancellationToken);
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
    
    private static async Task<IResult> RequestToSpent([FromBody] RequestToSpentDto requestToSpentDto,
        [FromRoute] int id, [FromServices] IPointService pointService,
        CancellationToken cancellationToken)
    {
        try
        {
            requestToSpentDto.CustomerId = IdentityExtensions.GetUserId();
            await pointService.RequestToSpent(requestToSpentDto, cancellationToken);
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