namespace Basket.Application.Services;

public class CatalogService : ICatalogService
{
    private readonly ApiClientSettings _catalogSettigns;
    private readonly ApiClientHelper _apiClientHelper;
    public CatalogService(
        IOptions<ApiClientSettings> catalogSettigns,
        ApiClientHelper apiClientHelper)
    {
        _catalogSettigns = catalogSettigns.Value;
        _apiClientHelper = apiClientHelper;
    }

    public async Task<CatalogItem> GetItemById(int id)
    {
        var itemResponse = await GetCatalogItemById(id);

        if (itemResponse == null || itemResponse.Id == 0)
        {
            throw new ArgumentException($"Item with id = {id} not found");
        }

        return itemResponse;
    }

    private async Task<CatalogItem> GetCatalogItemById(int id)
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_catalogSettigns);
        var response = await apiClient.GetAsync($"{_catalogSettigns.ApiUrl}/items/{id}");

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CatalogItem>(content);
        return result;
    }
}
