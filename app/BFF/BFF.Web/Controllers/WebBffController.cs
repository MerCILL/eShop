namespace BFF.Web.Controllers;

[ApiController]
[Route("/bff/catalog")]
[Authorize(Policy = "ApiScope")]
public class WebBffController : ControllerBase
{
    private readonly ICatalogBrandService _catalogBrandService;

    public WebBffController(ICatalogBrandService catalogBrandService)
    {
           _catalogBrandService = catalogBrandService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int page = 1, int size = 3)
    {
        var brands = await _catalogBrandService.Get(page, size);
        return Ok(brands);
    }
}
