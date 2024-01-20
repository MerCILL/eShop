namespace Catalog.Core.Interfaces;

public interface ICatalogTypeRepository : IGenericRepository<CatalogTypeEntity>
{
    Task<CatalogTypeEntity> Update(CatalogTypeEntity entity);
}