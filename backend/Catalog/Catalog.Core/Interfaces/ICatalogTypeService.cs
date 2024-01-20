namespace Catalog.Core.Interfaces;

public interface ICatalogTypeService
{
    Task<int> Add(CatalogType type);
    Task<int> Delete(int id);
    Task<IEnumerable<CatalogType>> Get();
    Task<CatalogType> GetById(int id);
    Task<CatalogType> Update(int id, string title);
}