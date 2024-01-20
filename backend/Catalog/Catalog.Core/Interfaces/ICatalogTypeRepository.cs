namespace Catalog.Core.Interfaces;

public interface ICatalogTypeRepository
{
    Task<int> Add(CatalogTypeEntity typeEntity);
    Task<int> Delete(int id);
    Task<IEnumerable<CatalogTypeEntity>> Get();
    Task<CatalogTypeEntity> GetById(int id);
    Task<CatalogTypeEntity> Update(int id, string title);
}