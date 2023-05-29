using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

namespace Modules.Identity.Web.Client
{
    public class AuthorizedHandler : DelegatingHandler
    {
        private readonly HostAuthenticationStateProvider authenticationStateProvider;
        public AuthorizedHandler(HostAuthenticationStateProvider authenticationStateProvider)
        {
            this.authenticationStateProvider = authenticationStateProvider;
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
