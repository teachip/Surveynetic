using MediatR;
using Microsoft.AspNetCore.Identity;
using Surveynetic.Service.Identity.Domain.Entities;
using Surveynetic.Shared.DTO.Account;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Surveynetic.Service.Identity.Application.CQRS.Account
{
    public class RegisterRequest : IRequest
    {
        public RegisterDto Dto { get; set; }
    }

    public class RegisterRequestHandler : IRequestHandler<RegisterRequest>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterRequestHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Dto.Email);
            if (userWithSameEmail is not null)
                throw new NotImplementedException();

            var user = new ApplicationUser
            {
                Email = request.Dto.Email,
                UserName = request.Dto.UserName
            };

            var registerResult = await _userManager.CreateAsync(user, request.Dto.Password);
            if (!registerResult.Succeeded)
                throw new NotImplementedException();

            return new Unit();
        }
    }
}
