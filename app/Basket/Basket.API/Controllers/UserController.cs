namespace Basket.API.Controllers;

[ApiController]
[Route("/api/v1/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

	public UserController(IUserService userService)
	{
		_userService = userService;
	}

    [HttpGet]
    public IActionResult GetActiveUser()
    {
        var userId = _userService.GetUserId(User);
        return Ok(userId);
    }
}
