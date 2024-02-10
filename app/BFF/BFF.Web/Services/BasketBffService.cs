using BFF.Web.Infrastructure.Settings;
using Helpers;
using Microsoft.Extensions.Options;
using Settings;

namespace BFF.Web.Services;

public class BasketBffService : IBasketBffService
{
    private readonly ApiClientSettings _basketSettings;
    private readonly ApiClientHelper _apiClientHelper;

    public BasketBffService(
        IOptions<BasketApiClientSettings> basketSettings,
        ApiClientHelper apiClientHelper)
    {
        _basketSettings = basketSettings.Value;
        _apiClientHelper = apiClientHelper;
    }

    public async Task<BasketResponse> GetBasket(string userId)
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_basketSettings);
        var response = await apiClient.GetAsync($"{_basketSettings.ApiUrl}/{userId}");

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<BasketResponse>(content);
        return result;
    }

    public async Task<int> AddBasketItem(ItemRequest itemRequest)
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_basketSettings);
        var response = await apiClient.PostAsJsonAsync($"{_basketSettings.ApiUrl}", itemRequest);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<int>(responseString);
        return result;
    }

    public async Task<DeleteBasketResponse> DeleteBasketItem(string userId, int itemId)
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_basketSettings);
        var response = await apiClient.DeleteAsync($"{_basketSettings.ApiUrl}/{userId}?itemId={itemId}");

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<DeleteBasketResponse>(responseString);
        return result;
    }
}