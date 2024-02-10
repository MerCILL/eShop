namespace BFF.Web.Services;

public class OrderBffService : IOrderBffService
{
    private readonly ApiClientSettings _orderSettings;
    private readonly ApiClientHelper _apiClientHelper;

    public OrderBffService(
        IOptions<OrderApiClientSettings> orderSettings,
        ApiClientHelper apiClientHelper
        )
    {
        _orderSettings = orderSettings.Value;
        _apiClientHelper = apiClientHelper;
    }

    public async Task<OrderResponse> GetOrderById(int id)
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_orderSettings);
        var response = await apiClient.GetAsync($"{_orderSettings.ApiUrl}/{id}");

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
        var apiClient = await _apiClientHelper.CreateClientWithToken(_orderSettings);
        var response = await apiClient.GetAsync($"http://localhost:5005/api/v1/users/{userId}/orders?page={page}&size={size}");

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
        var apiClient = await _apiClientHelper.CreateClientWithToken(_orderSettings);

        var response = await apiClient.GetAsync($"{_orderSettings.ApiUrl}?page={page}&size={size}");

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
        var apiClient = await _apiClientHelper.CreateClientWithToken(_orderSettings);
       
        var response = await apiClient.PostAsJsonAsync($"{_orderSettings.ApiUrl}", orderRequest);

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
        var apiClient = await _apiClientHelper.CreateClientWithToken(_orderSettings);

        var response = await apiClient.DeleteAsync($"{_orderSettings.ApiUrl}/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OrderResponse>(content);
            return result;
        }

        return null;
    }
}
