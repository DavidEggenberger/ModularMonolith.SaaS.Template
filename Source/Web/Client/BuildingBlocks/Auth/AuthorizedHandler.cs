using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using Web.Client.BuildingBlocks.Auth.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Web.Client.BuildingBlocks.Layouts.MainLayout;
using System.Text.Json;
using Shared.Client.Components.Modals;
using Blazored.Modal;

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

            if (responseMessage.StatusCode == HttpStatusCode.InternalServerError)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(await responseMessage.Content.ReadAsStringAsync());

                var parameters = new ModalParameters
                {
                    { nameof(ErrorModal.ModalExitedCallback), () => { MainLayout.ModalReference.Close(); } },
                    { nameof(ErrorModal.Title), problemDetails.Title },
                    { nameof(ErrorModal.Detail), problemDetails.Detail }
                };

                MainLayout.ModalReference = MainLayout.ModalService.Show<ErrorModal>(string.Empty, parameters, DefaultModalOptions.Modal);
            }

            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                authenticationStateProvider.SignIn();
            }

            return responseMessage;
        }
    }
}
