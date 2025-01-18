using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using Shared.Client.Types;
using Blazored.Modal;
using Shared.Client.Components.Modals;
using System.Net;
using Shared.Client.Layouts.MainLayout;

namespace Shared.Client.BuildingBlocks.Http
{
    public class AuthorizedHttpClientService
    {
        private HttpClient httpClient;
        public AuthorizedHttpClientService(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient(HttpClientConstants.AuthenticatedHttpClient);
            httpClient.BaseAddress = new Uri(httpClient.BaseAddress.ToString());
        }
        public async Task<T> GetFromAPIAsync<T>(string route)
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("/api" + route);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                try
                {
                    return JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                catch (Exception ex)
                {

                }
            }
            if (httpResponseMessage.IsSuccessStatusCode is false)
            {
                ProblemDetails problemDetails = JsonSerializer.Deserialize<ProblemDetails>(await httpResponseMessage.Content.ReadAsStringAsync());

                var parameters = new ModalParameters
                {
                    { nameof(ErrorModal.ModalExitedCallback), () => { MainLayout.ModalReference.Close(); } },
                    { nameof(ErrorModal.Title), problemDetails.Title },
                    { nameof(ErrorModal.Detail), problemDetails.Detail }
                };

                MainLayout.ModalReference = MainLayout.ModalService.Show<ErrorModal>(string.Empty, parameters, DefaultModalOptions.Modal);

                throw new HttpClientServiceException(problemDetails.Detail);
            }
            return default;
        }

        public async Task<T> PostToAPIAsync<T>(string route, T t)
        {
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync("/api" + route, t, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                try
                {
                    return JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                catch (Exception ex)
                {

                }
            }
            if (httpResponseMessage.IsSuccessStatusCode is false)
            {
                ProblemDetails problemDetails = JsonSerializer.Deserialize<ProblemDetails>(await httpResponseMessage.Content.ReadAsStringAsync());
                throw new HttpClientServiceException(problemDetails.Detail);
            }
            return default;
        }

        public async Task DeleteFromAPIAsync(string route, Guid id)
        {
            HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync("/api" + route + "/" + id);
            if (httpResponseMessage.IsSuccessStatusCode)
            {

            }
            if (httpResponseMessage.IsSuccessStatusCode is false)
            {
                ProblemDetails problemDetails = JsonSerializer.Deserialize<ProblemDetails>(await httpResponseMessage.Content.ReadAsStringAsync());
                throw new HttpClientServiceException(problemDetails.Detail);
            }
        }

        public void AddDefaultHeader(string name, string value)
        {
            httpClient.DefaultRequestHeaders.Add(name, value);
        }
    }
}

