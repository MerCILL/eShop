namespace BFF.Web.Services.Abstractions;

public interface IUserBffService
{
    string GetUserId(ClaimsPrincipal user);
}