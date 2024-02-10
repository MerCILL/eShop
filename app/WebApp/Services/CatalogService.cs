using Helpers;
using Microsoft.Extensions.Options;
using Settings;
using WebApp.Infrastructure.Settings;

namespace WebApp.Services;

public class CatalogService : ICatalogService
{
    private readonly ApiClientSettings _bffClientSettings;
    private readonly ApiClientHelper _apiClientHelper;

    public CatalogService(
        IOptions<MvcApiClientSettings> bffClientSettings,
        ApiClientHelper apiClientHelper
        )
    {
        _bffClientSettings = bffClientSettings.Value;
        _apiClientHelper = apiClientHelper;
    }

    public async Task<PaginatedDataModel<CatalogItemModel>> GetCatalogItems(int page, int size, string sort, List<int> types = null, List<int> brands = null)
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);
        var response = await apiClient.GetAsync($"{_bffClientSettings.ApiUrl}/catalog/items?page={page}&size={size}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaginatedDataModel<CatalogItemModel>>(content);

            if (types != null && types.Any())
            {
                result.Data = result.Data.Where(item => types.Contains(item.Type.Id)).ToList();
            }
            if (brands != null && brands.Any())
            {
                result.Data = result.Data.Where(item => brands.Contains(item.Brand.Id)).ToList();
            }


            switch (sort)
            {
                case "price_asc":
                    result.Data = result.Data.OrderBy(item => item.Price).ToList();
                    break;
                case "price_desc":
                    result.Data = result.Data.OrderByDescending(item => item.Price).ToList();
                    break;
                case "name_asc":
                    result.Data = result.Data.OrderBy(item => item.Title).ToList();
                    break;
                case "name_desc":
                    result.Data = result.Data.OrderByDescending(item => item.Title).ToList();
                    break;
                case "date_asc":
                    result.Data = result.Data.OrderBy(item => item.CreatedAt).ToList();
                    break;
                case "date_desc":
                    result.Data = result.Data.OrderByDescending(item => item.CreatedAt).ToList();
                    break;
            }

            return result;
        }
        else
        {
            throw new Exception("API request error");
        }
    }

    public async Task<PaginatedDataModel<CatalogTypeModel>> GetCatalogTypes()
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);
        var response = await apiClient.GetAsync($"{_bffClientSettings.ApiUrl}/catalog/types");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaginatedDataModel<CatalogTypeModel>>(content);
            return result;
        }
        else
        {
            throw new Exception("API request error");
        }
    }

    public async Task<PaginatedDataModel<CatalogBrandModel>> GetCatalogBrands()
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);
        var response = await apiClient.GetAsync($"{_bffClientSettings.ApiUrl}/catalog/brands");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PaginatedDataModel<CatalogBrandModel>>(content);
            return result;
        }
        else
        {
            throw new Exception("API request error");
        }
    }

    public async Task<CatalogItemModel> GetCatalogItemById(int id)
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);
        var response = await apiClient.GetAsync($"{_bffClientSettings.ApiUrl}/catalog/items/{id}");
     
        if (response.IsSuccessStatusCode) 
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CatalogItemModel>(content);
            return result;
        }
        else
        {
            throw new Exception("API request error");
        }
    }
}
