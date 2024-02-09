namespace BFF.Web.Services;

public class BasketBffService : IBasketBffService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BasketBffService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<BasketResponse> GetBasket(string userId)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");


        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "basket_api_client",
            ClientSecret = "basket_api_client_secret",
            Scope = "BasketAPI",
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);
;
        var response = await apiClient.GetAsync($"http://localhost:5004/api/v1/basket/{userId}");

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<BasketResponse>(content);
        return result;
    }

    public async Task<int> AddBasketItem(ItemRequest itemRequest)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "basket_api_client",
            ClientSecret = "basket_api_client_secret",
            Scope = "BasketAPI",
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.PostAsJsonAsync($"http://localhost:5004/api/v1/basket", itemRequest);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to add item: {response.ReasonPhrase}");
        }

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<int>(responseString);
        return result;
    }

    public async Task<DeleteBasketResponse> DeleteBasketItem(string userId, int itemId)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "basket_api_client",
            ClientSecret = "basket_api_client_secret",
            Scope = "BasketAPI",
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.DeleteAsync($"http://localhost:5004/api/v1/basket/{userId}?itemId={itemId}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to delete item: {response.ReasonPhrase}");
        }

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<DeleteBasketResponse>(responseString);
        return result;
    }

}