namespace Catalog.Core.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        Task<int> Add(TEntity entity);
        Task<int> Delete(int id);
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> GetById(int id);
        Task<TEntity> Update(TEntity entity);
    }
}