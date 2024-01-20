namespace Catalog.Core.Repositories;

public class CatalogTypeRepository : ICatalogTypeRepository
{
    private readonly CatalogDbContext _dbContext;

    public CatalogTypeRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CatalogTypeEntity>> Get()
    {
        var typesEntities = await _dbContext.Types.AsNoTracking().ToListAsync();

        return typesEntities;
    }

    public async Task<CatalogTypeEntity> GetById(int id)
    {
        var typeEntity = await _dbContext.Types.AsNoTracking().FirstOrDefaultAsync(type => type.Id == id);

        return typeEntity;
    }

    public async Task<int> Add(CatalogTypeEntity typeEntity)
    {
        await _dbContext.AddAsync(typeEntity);
        await _dbContext.SaveChangesAsync();

        return typeEntity.Id;
    }

    public async Task<CatalogTypeEntity> Update(int id, string title)
    {
        await _dbContext.Types
            .Where(type => type.Id == id)
            .ExecuteUpdateAsync(sp => sp
            .SetProperty(p => p.Title, p => title)
            .SetProperty(p => p.UpdatedAt, p => DateTime.UtcNow));

        var type = await _dbContext.Types
            .FirstOrDefaultAsync(type => type.Id == id);

        return type;
    }

    public async Task<int> Delete(int id)
    {
        var type = await _dbContext.Types
            .Where(type => type.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
