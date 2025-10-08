using AuthApi.common.Models;

namespace AuthApi.Services
{
    public interface ITokenService
    {
        string CreateAccessToken(ApplicationUser User, IList<string> roles);
        RefreshToken CreateRefreshToken(string ipAddress);
    }
}
