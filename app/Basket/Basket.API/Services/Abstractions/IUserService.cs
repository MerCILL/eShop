using System.Security.Claims;

namespace Basket.API.Services.Abstractions;

public interface IUserService
{
    string GetUserId(ClaimsPrincipal user);
}