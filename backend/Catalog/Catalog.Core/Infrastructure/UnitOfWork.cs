namespace Catalog.Core.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly CatalogDbContext _context;
    private Dictionary<Type, object> _repositories;
    private IDbContextTransaction _transaction;

    public UnitOfWork(CatalogDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
        _transaction = _context.Database.BeginTransaction();
    }

    public void Commit()
    {
        _context.SaveChanges();
        _transaction.Commit();
    }

    public void Rollback()
    {
        _transaction.Rollback();
    }

    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return (IGenericRepository<TEntity>)_repositories[typeof(TEntity)];
        }

        var repository = new GenericRepository<TEntity>(_context);
        _repositories.Add(typeof(TEntity), repository);
        return repository;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}