using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Surveynetic.Service.Identity.Application.Interfaces;
using Surveynetic.Service.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Surveynetic.Service.Identity.Infrastructure.Managers
{
    public class JWTManager : IJWTManager
    {
        private readonly IConfiguration _configuration;

        public JWTManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user, IList<Claim> userClaims, IList<string> userRoles)
        {
            var roleClaims = new List<Claim>();
            for (var i = 0; i < userRoles.Count; i++) roleClaims.Add(new Claim("roles", userRoles[i]));
            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id.ToString())
                }
                .Union(userClaims)
                .Union(roleClaims);

            var jwtSettings = _configuration.GetSection("JWT");
            var key = jwtSettings.GetValue<string>("Key");
            var issuer = jwtSettings.GetValue<string>("Issuer");
            var audience = jwtSettings.GetValue<string>("Audience");
            var durationInMinutes = jwtSettings.GetValue<double>("DurationInMinutes");

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(durationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
