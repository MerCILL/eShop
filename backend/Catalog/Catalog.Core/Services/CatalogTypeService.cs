namespace Catalog.Core.Services;

public class CatalogTypeService : ICatalogTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<CatalogTypeEntity> _catalogTypeRepository;

    public CatalogTypeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _catalogTypeRepository = _unitOfWork.GetRepository<CatalogTypeEntity>();
    }

    public async Task<IEnumerable<CatalogType>> Get()
    {
        var typesEntities = await _catalogTypeRepository.Get();

        var types = typesEntities.Select(type =>
        new CatalogType(
            type.Id,
            type.Title,
            type.CreatedAt,
            type.UpdatedAt));

        return types;
    }

    public async Task<CatalogType> GetById(int id)
    {
        var typeEntity = await _catalogTypeRepository.GetById(id);

        if (typeEntity == null)
        {
            return null;
        }

        var type = new CatalogType(
            typeEntity.Id,
            typeEntity.Title,
            typeEntity.CreatedAt,
            typeEntity.UpdatedAt);

        return type;
    }

    public async Task<int> Add(CatalogType type)
    {
        var typeEntity = new CatalogTypeEntity
        {
            Id = type.Id,
            Title = type.Title,
            CreatedAt = type.CreatedAt,
            UpdatedAt = type.UpdatedAt,
        };

        await _catalogTypeRepository.Add(typeEntity);
        _unitOfWork.Commit();

        return typeEntity.Id;
    }

    public async Task<CatalogType> Update(int id, string title)
    {
        var typeEntity = new CatalogTypeEntity
        {
            Id = id,
            Title = title,
            UpdatedAt = DateTime.UtcNow
        };

        typeEntity = await _catalogTypeRepository.Update(typeEntity);
        _unitOfWork.Commit();

        var type = new CatalogType(
            typeEntity.Id,
            typeEntity.Title,
            typeEntity.CreatedAt,
            typeEntity.UpdatedAt);

        return type;
    }

    public async Task<int> Delete(int id)
    {
        var result = await _catalogTypeRepository.Delete(id);
        _unitOfWork.Commit();

        return result;
    }
}
