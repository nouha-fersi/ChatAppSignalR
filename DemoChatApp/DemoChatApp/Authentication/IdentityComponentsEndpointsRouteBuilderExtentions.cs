using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace DemoChatApp.Authentication
{
    public static class IdentityComponentsEndpointsRouteBuilderExtentions
    {
        public static IEndpointConventionBuilder MapAdditionalIdentityEnpoints(this IEndpointRouteBuilder endpoints) 
        {
            var accountGroup = endpoints.MapGroup("/Acount");
            accountGroup.MapPost("/Logout",async (ClaimsPrincipal user , SignInManager <AppUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return TypedResults.LocalRedirect("/");

            });
            return accountGroup;
        }
    }
}
