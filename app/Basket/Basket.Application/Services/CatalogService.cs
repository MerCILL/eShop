using Microsoft.Extensions.Options;

namespace Basket.Application.Services;

public class CatalogService : ICatalogService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ApiClientSettings _apiClientSettigns;
    public CatalogService(
        IHttpClientFactory httpClientFactory, 
        IOptions<ApiClientSettings> apiClientSettings)
    {
        _httpClientFactory = httpClientFactory;
        _apiClientSettigns = apiClientSettings.Value;
    }

    public async Task<CatalogItem> GetItemById(int id)
    {
        var itemResponse = await GetCatalogResponseById("items", id);

        if (itemResponse == null || itemResponse.Id == 0)
        {
            throw new ArgumentException($"Item with id = {id} not found");
        }

        return itemResponse;
    }

    private async Task<CatalogItem> GetCatalogResponseById(string endpoint, int id)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync(_apiClientSettigns.DiscoveryUrl);

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = _apiClientSettigns.ClientId,
            ClientSecret = _apiClientSettigns.ClientSecret,
            Scope = _apiClientSettigns.Scope
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.GetAsync($"{_apiClientSettigns.ApiUrl}/{endpoint}/{id}");

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CatalogItem>(content);
        return result;
    }
}
