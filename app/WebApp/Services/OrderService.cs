namespace WebApp.Services;

public class OrderService : IOrderService
{
    private readonly ApiClientSettings _bffClientSettings;
    private readonly ApiClientHelper _apiClientHelper;

    public OrderService(
        IOptions<MvcApiClientSettings> bffClientSettings,
        ApiClientHelper apiClientHelper)
    {
        _bffClientSettings = bffClientSettings.Value;
        _apiClientHelper = apiClientHelper;
    }

    public async Task<IEnumerable<OrderModel>> GetOrdersByUser(HttpContext httpContext)
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);

        var userId = FindUserId(httpContext);

        var response = await apiClient.GetAsync($"{_bffClientSettings.ApiUrl}/users/{userId}/orders");

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<IEnumerable<OrderModel>>(content);
        return result;
    }

    public async Task<OrderModel> AddOrder(OrderRequest orderRequest)
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);

        var response = await apiClient.PostAsJsonAsync($"{_bffClientSettings.ApiUrl}/orders", orderRequest);

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<OrderModel>(content);
        return result;
    }

    public string FindUserId(HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "sub");
        return userIdClaim.Value;
    }
}



