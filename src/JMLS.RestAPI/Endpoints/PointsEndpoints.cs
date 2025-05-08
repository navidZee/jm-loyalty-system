namespace JMLS.RestAPI.Endpoints;

public static class PointsEndpoints
{
    public static WebApplication MapPointEndpoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/points")
            .WithTags("points")
            .WithDescription("Set points")
            .WithOpenApi();

        _ = root.MapGet("/hello", SayHello)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Say Hello")
            .WithDescription("\n    GET api/points/hello");

        return app;
    }

    private static Task<IResult> SayHello()
    {
        return Task.FromResult(Results.Ok("Hello World!"));
    }
}