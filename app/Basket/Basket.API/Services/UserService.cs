using System.Security.Claims;
using Basket.API.Services.Abstractions;

namespace Basket.API.Services;

public class UserService : IUserService
{
    public string GetUserId(ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
