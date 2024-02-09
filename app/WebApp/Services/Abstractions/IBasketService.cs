using System.Security.Claims;
using WebApp.Models;
using WebApp.Responses;

namespace WebApp.Services.Abstractions;

public interface IBasketService
{
    Task<BasketModel> GetBasket(HttpContext httpContext);

    string FindUserId(HttpContext httpContext);
    Task<int> AddBasketItem(ItemRequest itemRequest);
    Task<DeleteBasketResponse> DeleteBasketItem(string userId, int itemId);
}