using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BFF.Web.Controllers;

[ApiController]
[Route("bff/users")]
[Authorize(Policy = "ApiScope")]
public class UserWebBffController : ControllerBase
{
    private readonly IUserBffService _userBffService;
    private readonly IOrderBffService _orderBffService;

    public UserWebBffController(
        IUserBffService userBffService, 
        IOrderBffService orderBffService)
    {
        _userBffService = userBffService;
        _orderBffService = orderBffService;
    }

    [HttpGet("me")]
    public IActionResult GetActiveUserId()
    {
        var userId = _userBffService.GetUserId(User);
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
