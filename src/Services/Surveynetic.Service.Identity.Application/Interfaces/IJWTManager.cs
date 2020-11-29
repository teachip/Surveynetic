using Surveynetic.Service.Identity.Domain.Entities;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Surveynetic.Service.Identity.Application.Interfaces
{
    public interface IJWTManager
    {
        Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user, IList<Claim> userClaims, IList<string> userRoles);
    }
}
