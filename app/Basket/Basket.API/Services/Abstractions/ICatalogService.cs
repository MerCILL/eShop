using Basket.API.Responses;

namespace Basket.API.Services.Abstractions;

public interface ICatalogService
{
    Task<CatalogItemResponse> GetItemById(int id);
}