namespace BFF.Web.Services.Abstractions;

public interface ICatalogService
{
    Task<PaginatedResponse<CatalogBrand>> GetBrands(int page = 1, int size = 3);
    Task<PaginatedResponse<CatalogType>> GetTypes(int page = 1, int size = 3);
    Task<PaginatedResponse<CatalogItem>> GetItems(int page = 1, int size = 10);
    Task<CatalogItem> GetItemById(int id);
}