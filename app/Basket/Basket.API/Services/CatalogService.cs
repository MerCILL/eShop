using Basket.API.Responses;
using Basket.API.Services.Abstractions;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace Basket.API.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CatalogService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CatalogItemResponse> GetItemById(int id)
        {
            var itemResponse = await GetCatalogResponseById<CatalogItemResponse>("items", id);

            if (itemResponse == null || itemResponse.Id == 0)
            {
                throw new ArgumentException($"Item with id = {id} not found");
            }

            return itemResponse;
        }

        private async Task<T> GetCatalogResponseById<T>(string endpoint, int id)
        {
            var client = _httpClientFactory.CreateClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "catalog_api_client",
                ClientSecret = "catalog_api_client_secret",
                Scope = "CatalogAPI"
            });

            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync($"http://localhost:5000/api/v1/catalog/{endpoint}/{id}");

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }
    }

}
