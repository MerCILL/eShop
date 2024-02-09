namespace BFF.Web.Services;

public class UserBffService : IUserBffService
{
    public string GetUserId(ClaimsPrincipal user)
    {
        Claim identifierClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        return identifierClaim?.Value;
    }
}
