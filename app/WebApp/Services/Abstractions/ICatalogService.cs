using WebApp.Models;

namespace WebApp.Services.Abstractions
{
    public interface ICatalogService
    {
        Task<PaginatedResponse<CatalogItemModel>> GetCatalogItems(int page, int size, string sort);
    }
}