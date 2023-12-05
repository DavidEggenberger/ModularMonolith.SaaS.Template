using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using Web.Client.BuildingBlocks.Auth.Antiforgery;

namespace Web.Client.BuildingBlocks.Auth
{
    public class AuthorizedHandler : DelegatingHandler
    {
        private readonly HostAuthenticationStateProvider authenticationStateProvider;
        private readonly AntiforgeryTokenService antiforgeryTokenService;
        public AuthorizedHandler(HostAuthenticationStateProvider authenticationStateProvider, AntiforgeryTokenService antiforgeryTokenService)
        {
            this.authenticationStateProvider = authenticationStateProvider;
            this.antiforgeryTokenService = antiforgeryTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            HttpResponseMessage responseMessage;
            if (!authState.User.Identity.IsAuthenticated)
            {
                // if user is not authenticated, immediately set response status to 401 Unauthorized
                responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                request.Headers.Add("X-XSRF-TOKEN", await antiforgeryTokenService.GetAntiforgeryTokenAsync());
                responseMessage = await base.SendAsync(request, cancellationToken);
            }

            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                authenticationStateProvider.SignIn();
            }

            return responseMessage;
        }
    }
}
