using MediatR;
using Microsoft.AspNetCore.Identity;
using Surveynetic.Service.Identity.Application.Interfaces;
using Surveynetic.Service.Identity.Domain.Entities;
using Surveynetic.Shared.DTO.Account;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;

namespace Surveynetic.Service.Identity.Application.CQRS.Account
{
    public class LoginRequest : IRequest<AuthDto>
    {
        public LoginDto Dto { get; set; }
    }

    public class LoginRequestHandler : IRequestHandler<LoginRequest, AuthDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJWTManager _jwtManager;

        public LoginRequestHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJWTManager jwtManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtManager = jwtManager;
        }

        public async Task<AuthDto> Handle(LoginRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(request.Dto.Email);
            if (user is null)
                throw new NotImplementedException();

            var authResult = await _signInManager.PasswordSignInAsync(user, request.Dto.Password, false, false);
            if (!authResult.Succeeded)
                throw new NotImplementedException();

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var jwtSecurityToken = await _jwtManager.GenerateJWToken(user, userClaims, userRoles);
            var response = new AuthDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName,
                IsVerified = user.EmailConfirmed
            };

            return response;
        }
    }
}
