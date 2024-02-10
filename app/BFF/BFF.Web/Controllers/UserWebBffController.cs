namespace BFF.Web.Controllers;

[ApiController]
[Route("bff/users")]
[Authorize(Policy = "ApiScope")]
public class UserWebBffController : ControllerBase
{
    private readonly IOrderBffService _orderBffService;

    public UserWebBffController(
        IOrderBffService orderBffService)
    {
        _orderBffService = orderBffService;
    }

    [HttpGet("me")]
    public IActionResult GetActiveUserId()
    {
        var userId = User.GetUserId();
        if (userId == null)
            return NotFound("User is null");
        else
            return Ok(userId);
    }

    [HttpGet("{userId}/orders")]
    public async Task<IActionResult> GetOrdersByUser(string userId, [FromQuery] int page = 1, [FromQuery] int size = 50)
    {
        var orders = await _orderBffService.GetOrdersByUser(userId, page, size);

        if (orders == null)
        {
            return NotFound();
        }

        return Ok(orders);
    }


}
