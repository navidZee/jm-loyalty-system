using System.Diagnostics.CodeAnalysis;
using JMLS.RestAPI.Endpoints;
using JMLS.RestAPI.Infrastructure.Persistence.SQL;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace JMLS.RestAPI.Extensions;

[ExcludeFromCodeCoverage]
public static class WebApplicationExtensions
{
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        _ = app.UseSerilogRequestLogging();

        _ = app.UseHsts();

        _ = app.UseHttpsRedirection();

        _ = app.UseSwagger();
        _ = app.UseSwaggerUI(c =>
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"JMLS - V1"));

        _ = app.MapPointEndpoints();

        return app;
    }
}