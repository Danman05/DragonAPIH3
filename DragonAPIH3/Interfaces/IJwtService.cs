using System.Security.Claims;

namespace DragonAPIH3.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string username);
        bool ValidateToken(string token, out ClaimsPrincipal principal);
    }
}
