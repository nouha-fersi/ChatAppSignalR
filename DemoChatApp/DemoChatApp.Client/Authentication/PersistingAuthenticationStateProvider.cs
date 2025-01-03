using System.Security.Claims;
using ChatModels.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace DemoChatApp.Client.Authentication
{
    public class PersistingAuthenticationStateProvider : AuthenticationStateProvider
    {
        private static readonly Task<AuthenticationState> defaultAuthenticatedTask =
            Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

        private readonly Task<AuthenticationState?> authenticationStateTask = defaultAuthenticatedTask!;

        public PersistingAuthenticationStateProvider(
     PersistentComponentState state )
        {
            if (!state.TryTakeFromJson<UserInfo>(nameof(UserInfo), out var userInfo) || userInfo is null)
                return;

            Claim[] claims =
            [
        new Claim(ClaimTypes.NameIdentifier, userInfo.Id ?? string.Empty),
        new Claim(ClaimTypes.Email, userInfo.Email ?? string.Empty),
        new Claim(ClaimTypes.Name, userInfo.Fullname ?? string.Empty)
    ];

            authenticationStateTask = Task.FromResult(
                new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, nameof(PersistingAuthenticationStateProvider)))))!;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
            => authenticationStateTask!;
    }
}
