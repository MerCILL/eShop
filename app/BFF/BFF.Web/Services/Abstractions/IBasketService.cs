namespace BFF.Web.Services.Abstractions;

public interface IBasketService
{
    Task<Basket> GetBasket(string userId);
    Task<int> AddBasketItem(ItemRequest item);
    Task<DeleteBasketResponse> DeleteBasketItem(string userId, int itemId);
}