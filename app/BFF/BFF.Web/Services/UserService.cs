namespace BFF.Web.Services;

public class UserService : IUserService
{
    public string GetUserId(ClaimsPrincipal user)
    {
        Claim identifierClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        return identifierClaim?.Value;
    }
}
