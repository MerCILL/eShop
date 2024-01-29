using Basket.API.Models;

namespace Basket.API.Services.Abstractions;

public interface IBasketService
{
    Task<Models.Basket> GetBasket(string userId);
    Task<BasketItem> AddItem(string userId, int id);
    Task<BasketItem> RemoveItem(string userId, int id);
}