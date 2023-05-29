using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using Shared.Kernel.BuildingBlocks.Authorization.Constants;
using Modules.TenantIdentity.Web.Shared;
using Modules.TenantIdentity.Web.Shared.DTOs;

namespace Modules.Identity.Web.Client
{
    public class HostAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly NavigationManager navigationManager;
        private readonly HttpClient httpClient;
        private static readonly TimeSpan UserCacheRefreshInterval = TimeSpan.FromSeconds(60);
        private DateTimeOffset userLastCheck = DateTimeOffset.FromUnixTimeSeconds(0);
        private ClaimsPrincipal cachedUser = new ClaimsPrincipal(new ClaimsIdentity());
        public HostAuthenticationStateProvider(NavigationManager navigationManager, HttpClient httpClient)
        {
            this.navigationManager = navigationManager;
            this.httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return new AuthenticationState(await GetUser());
        }

        private async ValueTask<ClaimsPrincipal> GetUser()
        {
            if (DateTime.Now < (userLastCheck + UserCacheRefreshInterval))
            {
                return cachedUser;
            }
            else
            {
                cachedUser = await FetchUser();
                userLastCheck = DateTime.Now;
                return cachedUser;
            }
        }

        private async Task<ClaimsPrincipal> FetchUser()
        {
            try
            {
                var response = await httpClient.GetAsync(EndpointConstants.UserClaimsPath);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var bffUserInfoDTO = await response.Content.ReadFromJsonAsync<BFFUserInfoDTO>();

                    var identity = new ClaimsIdentity(
                        nameof(HostAuthenticationStateProvider),
                        ClaimConstants.UserNameClaimType,
                        ClaimConstants.UserRoleInTenantClaimType);

                    foreach (var claim in bffUserInfoDTO.Claims)
                    {
                        identity.AddClaim(new Claim(claim.Type, claim.Value.ToString()));
                    }

                    return new ClaimsPrincipal(identity);
                }
            }
            catch (Exception ex)
            {

            }

            return new ClaimsPrincipal(new ClaimsIdentity());
        }

        public void SignIn(string customReturnUrl = null)
        {
            var encodedReturnUrl = Uri.EscapeDataString(customReturnUrl ?? navigationManager.Uri);
            var logInUrl = navigationManager.ToAbsoluteUri($"{EndpointConstants.LoginPath}?returnUrl={encodedReturnUrl}");
            navigationManager.NavigateTo(logInUrl.ToString(), true);
        }

        public void SignUp(string customReturnUrl = null)
        {
            var encodedReturnUrl = Uri.EscapeDataString(customReturnUrl ?? navigationManager.Uri);
            var logInUrl = navigationManager.ToAbsoluteUri($"{EndpointConstants.SignUpPath}?returnUrl={encodedReturnUrl}");
            navigationManager.NavigateTo(logInUrl.ToString(), true);
        }

        public void GoToManageAccount()
        {
            navigationManager.NavigateTo(EndpointConstants.IdentityAccountPath, true);
        }
        public void SignOut()
        {
            navigationManager.NavigateTo(EndpointConstants.LogoutPath, true);
        }
    }
}
