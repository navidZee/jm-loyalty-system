using JMLS.RestAPI.Extensions;

var builder = WebApplication.CreateBuilder(args)
    .ConfigureApplicationBuilder();

var app = builder.Build()
    .ConfigureApplication();

app.UseHttpsRedirection();
app.UseAuthentication();  // Authentication must come before Authorization
app.UseAuthorization();

app.Run();