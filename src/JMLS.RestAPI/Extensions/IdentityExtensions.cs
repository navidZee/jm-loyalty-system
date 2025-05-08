using System.Security.Claims;
using JMLS.RestAPI.Services;

namespace JMLS.RestAPI.Extensions;

public static class IdentityExtensions
{
    private static string GetUsername(this ClaimsPrincipal user)
    {
        return user.FindFirstValue("preferred_username") ??
               throw new UnauthorizedAccessException("Username claim not found");
    }

    public static async Task<int> GetOrCreateUserIdAsync(this ClaimsPrincipal user, ICustomerService customerService)
    {
        var username = user.GetUsername();
        var customer = await customerService.GetByUserNameAsync(username);

        if (customer == null)
        {
            customer = await customerService.CreateAsync(username);
        }

        return customer.Id;
    }
}