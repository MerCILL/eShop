namespace BFF.Web.Controllers;

[ApiController]
[Route("/bff/catalog")]
[Authorize(Policy = "ApiScope")]
public class CatalogBffController : ControllerBase
{
    private readonly ICatalogBffService _catalogBffService;

    public CatalogBffController(ICatalogBffService catalogBffService)
    {
        _catalogBffService = catalogBffService;
    }

    [HttpGet("brands")]
    public async Task<IActionResult> GetBrands(int page = 1, int size = 3)
    {
        try
        {
            var brands = await _catalogBffService.GetBrands(page, size);
            return Ok(brands);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetTypes(int page = 1, int size = 3)
    {
        try
        {
            var types = await _catalogBffService.GetTypes(page, size);
            return Ok(types);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpGet("items")]
    public async Task<IActionResult> GetItems(int page = 1, int size = 10)
    {
        try
        {
            var items = await _catalogBffService.GetItems(page, size);
            return Ok(items);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpGet("items/{id}")]
    public async Task<IActionResult> GetItemById(int id)
    {
        var item = await _catalogBffService.GetItemById(id);
        return Ok(item);
    }
}
