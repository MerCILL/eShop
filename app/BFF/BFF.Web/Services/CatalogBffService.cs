namespace BFF.Web.Services;

public class CatalogBffService : ICatalogBffService
{
    private readonly ILogger<CatalogBffService> _logger;
    private readonly ApiClientSettings _catalogSettings;
    private readonly ApiClientHelper _apiClientHelper;

    public CatalogBffService(
        ILogger<CatalogBffService> logger,
        IOptions<CatalogApiClientSettings> catalogSettings,
        ApiClientHelper apiClientHelper)
    {
        _logger = logger;
        _catalogSettings = catalogSettings.Value;
        _apiClientHelper = apiClientHelper;
    }

    public async Task<PaginatedResponse<CatalogBrandResponse>> GetBrands(int page, int size)
    {
        return await GetPaginatedResponse<CatalogBrandResponse>("brands", page, size);
    }

    public async Task<PaginatedResponse<CatalogTypeResponse>> GetTypes(int page, int size)
    {
        return await GetPaginatedResponse<CatalogTypeResponse>("types", page, size);
    }

    public async Task<PaginatedResponse<CatalogItemResponse>> GetItems(int page, int size)
    {
        return await GetPaginatedResponse<CatalogItemResponse>("items", page, size);
    }

    public async Task<CatalogItemResponse> GetItemById(int id)
    {
        return await GetUnitById<CatalogItemResponse>("items", id);
    }

    private async Task<PaginatedResponse<T>> GetPaginatedResponse<T>(string endpoint, int page, int size)
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_catalogSettings);
        var response = await apiClient.GetAsync($"{_catalogSettings.ApiUrl}/{endpoint}?page={page}&size={size}");

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<PaginatedResponse<T>>(content);
        return result;
    }

    private async Task<T> GetUnitById<T>(string endpoint, int id)
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_catalogSettings);
        var response = await apiClient.GetAsync($"{_catalogSettings.ApiUrl}/{endpoint}/{id}");

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<T>(content);
        return result;
    }
}



