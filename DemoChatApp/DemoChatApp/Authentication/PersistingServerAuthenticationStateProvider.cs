using System.Security.Claims;
using DemoChatApp.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DemoChatApp.Authentication
{
    public class PersistingServerAuthenticationStateProvider : ServerAuthenticationStateProvider, IDisposable
    {
        private readonly PersistentComponentState state;
        private readonly IdentityOptions options;
        private readonly PersistingComponentStateSubscription subscription;
        private Task<AuthenticationState>? authenticationStateTask;

        public PersistingServerAuthenticationStateProvider(
            PersistentComponentState persistentComponentState ,
            IOptions<IdentityOptions> optionsAccessor)
        {
            state = persistentComponentState;
            options= optionsAccessor.Value;

            AuthenticationStateChanged += OnAuthenticationStateChanged;
            subscription = state.RegisterOnPersisting(OnpersistingAsync, RenderMode.InteractiveWebAssembly);
        }

       

        private void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            authenticationStateTask = task;
        }

        private async Task OnpersistingAsync()
        {
            AuthenticationState authenticationState = await authenticationStateTask!;
            var principal = authenticationState.User;
            if (principal.Identity?.IsAuthenticated == true)
            {
                var userId = principal.FindFirst(options.ClaimsIdentity.UserIdClaimType)?.Value;
                var email = principal.FindFirst(options.ClaimsIdentity.EmailClaimType)?.Value;
                var fullname = principal.Claims.Where(f => f.Type == ClaimTypes.Name).Last().Value;

                if (userId != null && email != null && fullname != null )
                {
                    state.PersistAsJson(nameof(UserInfo), new UserInfo
                    {
                        Id =userId,
                        Fullname = fullname,
                        Email = email
                    });
                }
            }
        }

        public void Dispose()
        {
            subscription.Dispose();
            AuthenticationStateChanged -= OnAuthenticationStateChanged;
        }
    }
}
