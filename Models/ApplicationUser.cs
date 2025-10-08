using Microsoft.AspNetCore.Identity;

namespace AuthApi.common.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<RefreshToken> RefreshTokens { get; set; } = new();
    }
}
