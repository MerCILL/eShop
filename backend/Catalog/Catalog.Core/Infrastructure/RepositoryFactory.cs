namespace Catalog.Core.Infrastructure;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly CatalogDbContext _dbContext;
    private readonly Dictionary<Type, Func<CatalogDbContext, object>> _factories;

    public RepositoryFactory(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
        _factories = new Dictionary<Type, Func<CatalogDbContext, object>>
        {
            { typeof(CatalogTypeEntity), ctx => new CatalogTypeRepository(ctx) },
            { typeof(CatalogBrandEntity), ctx => new CatalogBrandRepository(ctx) },
            { typeof(CatalogItemEntity), ctx => new CatalogItemRepository(ctx) },
        };
    }

    public IGenericRepository<TEntity> CreateRepository<TEntity>() where TEntity : class, IEntity
    {
        if (_factories.TryGetValue(typeof(TEntity), out var factory))
        {
            return (IGenericRepository<TEntity>)factory(_dbContext);
        }

        return new GenericRepository<TEntity>(_dbContext);
    }
}
