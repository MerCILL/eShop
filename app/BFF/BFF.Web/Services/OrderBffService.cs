namespace BFF.Web.Services;

public class OrderBffService : IOrderBffService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OrderBffService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<OrderResponse> GetOrderById(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "order_api_client",
            ClientSecret = "order_api_client_secret",
            Scope = "OrderAPI",
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.GetAsync($"http://localhost:5005/api/v1/orders/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OrderResponse>(content);
            return result;
        }

        return null;
    }

    public async Task<IEnumerable<OrderResponse>> GetOrdersByUser(string userId, int page, int size)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "order_api_client",
            ClientSecret = "order_api_client_secret",
            Scope = "OrderAPI",
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.GetAsync($"http://localhost:5005/api/v1/orders/users/{userId}?page={page}&size={size}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<OrderResponse>>(content);
            return result;
        }

        return null;
    }

    public async Task<IEnumerable<OrderResponse>> GetOrders(int page, int size)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "order_api_client",
            ClientSecret = "order_api_client_secret",
            Scope = "OrderAPI",
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.GetAsync($"http://localhost:5005/api/v1/orders?page={page}&size={size}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<OrderResponse>>(content);
            return result;
        }

        return null;
    }

    public async Task<OrderResponse> AddOrder(OrderRequest orderRequest)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "order_api_client",
            ClientSecret = "order_api_client_secret",
            Scope = "OrderAPI",
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.PostAsJsonAsync($"http://localhost:5005/api/v1/orders", orderRequest);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OrderResponse>(content);
            return result;
        }

        return null;
    }

    public async Task<OrderResponse> DeleteOrder(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "order_api_client",
            ClientSecret = "order_api_client_secret",
            Scope = "OrderAPI",
        });

        var apiClient = _httpClientFactory.CreateClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.DeleteAsync($"http://localhost:5005/api/v1/orders/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OrderResponse>(content);
            return result;
        }

        return null;
    }
}
