using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Modules.TenantIdentity.Web.Shared.DTOs.IdentityOperations;
using Shared.Kernel.BuildingBlocks.Auth.Constants;
using System.Net.Http;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using Shared.Kernel.Constants.Endpoints;

namespace Web.Client.BuildingBlocks.Auth
{
    public class HostAuthenticationStateProvider : AuthenticationStateProvider
    {
        //private readonly AntiforgeryTokenService antiforgeryTokenService;
        private readonly NavigationManager navigationManager;
        private readonly HttpClient httpClient;
        private static readonly TimeSpan UserCacheRefreshInterval = TimeSpan.FromSeconds(60);
        private DateTimeOffset userLastCheck = DateTimeOffset.FromUnixTimeSeconds(0);
        private ClaimsPrincipal cachedUser = new ClaimsPrincipal(new ClaimsIdentity());
        public HostAuthenticationStateProvider(NavigationManager navigationManager, HttpClient httpClient)
        {
            this.navigationManager = navigationManager;
            this.httpClient = httpClient;
            //this.antiforgeryTokenService = antiforgeryTokenService;
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
                var response = await httpClient.GetAsync(EndpointConstants.TenantIdentity.UserClaimsPath);

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
            var logInUrl = navigationManager.ToAbsoluteUri($"{EndpointConstants.TenantIdentity.LoginPath}?returnUrl={encodedReturnUrl}");
            navigationManager.NavigateTo(logInUrl.ToString(), true);
        }

        public void SignUp(string customReturnUrl = null)
        {
            var encodedReturnUrl = Uri.EscapeDataString(customReturnUrl ?? navigationManager.Uri);
            var logInUrl = navigationManager.ToAbsoluteUri($"{EndpointConstants.TenantIdentity.SignUpPath}?returnUrl={encodedReturnUrl}");
            navigationManager.NavigateTo(logInUrl.ToString(), true);
        }

        public void GoToManageAccount()
        {
            navigationManager.NavigateTo(EndpointConstants.TenantIdentity.IdentityAccountPath, true);
        }
        public void SignOut()
        {
            navigationManager.NavigateTo(EndpointConstants.TenantIdentity.LogoutPath, true);
        }
    }
}
