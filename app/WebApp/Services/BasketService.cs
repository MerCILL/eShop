using Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Settings;
using System.Drawing;
using WebApp.Infrastructure.Settings;

namespace WebApp.Services;

public class BasketService : IBasketService
{
    private readonly ApiClientSettings _bffClientSettings;
    private readonly ApiClientHelper _apiClientHelper;

    public BasketService(
        IOptions<MvcApiClientSettings> bffClientSettings,
        ApiClientHelper apiClientHelper)
    {
        _bffClientSettings = bffClientSettings.Value;
        _apiClientHelper = apiClientHelper;
    }

    public async Task<BasketModel> GetBasket(HttpContext httpContext)
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);

        var userId = FindUserId(httpContext);

        var response = await apiClient.GetAsync($"{_bffClientSettings.ApiUrl}/basket/{userId}");

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<BasketModel>(content);
        return result;
    }

    public async Task<int> AddBasketItem(ItemRequest itemRequest)
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);

        var response = await apiClient.PostAsJsonAsync($"{_bffClientSettings.ApiUrl}/basket", itemRequest);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<int>(responseString);
        return result;
    }

    public async Task<DeleteBasketResponse> DeleteBasketItem(string userId, int itemId)
    {
        var apiClient = await _apiClientHelper.CreateClientWithToken(_bffClientSettings);

        var response = await apiClient.DeleteAsync($"{_bffClientSettings.ApiUrl}/basket/{userId}?itemId={itemId}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to delete item: {response.ReasonPhrase}");
        }

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<DeleteBasketResponse>(responseString);
        return result;
    }

    public string FindUserId(HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "sub");
        return userIdClaim.Value;
    }
}
