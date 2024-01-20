namespace Catalog.API.Controllers;

[ApiController]
[Route("/api/v1/catalog/types")]
public class CatalogTypeController : ControllerBase
{
    private readonly ICatalogTypeService _catalogTypeService;

    public CatalogTypeController(ICatalogTypeService catalogTypeService)
    {
        _catalogTypeService = catalogTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTypes()
    {
        var types = await _catalogTypeService.Get();

        var response = types.Select(type => new CatalogTypeGetResponse(
           type.Id, 
           type.Title,
           type.CreatedAt,
           type.UpdatedAt));

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTypeById(int id)
    {
        var type = await _catalogTypeService.GetById(id);

        if (type == null)
        {
            return Ok("null");
        }

        var response = new CatalogTypeGetResponse(
            type.Id,
            type.Title,
            type.CreatedAt,
            type.UpdatedAt);

        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> AddType([FromBody] CatalogTypeRequest request)
    {
        var type = new CatalogType(
            request.Title,
            DateTime.UtcNow,
            null
            );

        var id = await _catalogTypeService.Add(type);

        return Ok($"Type with id = {id} was succesfully created");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateType(int id, [FromBody] CatalogTypeRequest request)
    {
        var type = await _catalogTypeService.Update(id, request.Title);

        var response = new CatalogTypeGetResponse(
            type.Id,
            type.Title,
            type.CreatedAt,
            type.UpdatedAt
        );

        return Ok($"Update type with id = {response.Id}: {response.Title}, {response.CreatedAt}, {response.UpdatedAt}");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteType(int id)
    {
        await _catalogTypeService.Delete(id);
        return Ok($"Type with id = {id} was succesfully deleted");
    }
}
