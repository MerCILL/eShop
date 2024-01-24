namespace Catalog.API.Controllers;

[ApiController]
[Route("/api/v1/catalog/items")]
public class CatalogItemController : Controller
{
    private readonly ICatalogItemService _catalogItemService;
    private readonly IMapper _mapper;

    public CatalogItemController(
        ICatalogItemService catalogItemService,
        IMapper mapper)
    {
        _catalogItemService = catalogItemService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetItems(int page = 1, int size = 3)
    {
        var items = await _catalogItemService.Get(page, size);
        var response = _mapper.Map<IEnumerable<CatalogItemResponse>>(items);

        var total = await _catalogItemService.Count();

        var paginatedResponse = new PaginatedResponse<CatalogItemResponse>(
            page,
            size,
            total,
            (int)Math.Ceiling(total / (double)size),
            response
        );

        return Ok(paginatedResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemById(int id)
    {
        var item = await _catalogItemService.GetById(id);

        var response = _mapper.Map<CatalogItemResponse>(item);

        return Ok(response);
    }
}


