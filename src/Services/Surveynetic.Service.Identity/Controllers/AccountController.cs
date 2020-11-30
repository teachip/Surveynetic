using MediatR;
using Microsoft.AspNetCore.Mvc;
using Surveynetic.Shared.DTO.Account;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Surveynetic.Service.Identity.Application.CQRS.Account;

namespace Surveynetic.Service.Identity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync([FromBody] LoginDto dto)
        {
            var result = await Mediator.Send(new LoginRequest { Dto = dto });
            return Ok(result);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync([FromBody] RegisterDto dto)
        {
            var result = await Mediator.Send(new RegisterRequest { Dto = dto });
            return Ok(result);
        }

        [HttpPost("signout")]
        public async Task<IActionResult> SignOutAsync([FromBody] RegisterDto dto)
        {
            var result = await Mediator.Send(new RegisterRequest { Dto = dto });
            return Ok(result);
        }
    }
}
