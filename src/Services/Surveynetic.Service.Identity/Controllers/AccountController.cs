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

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
        {
            var result = await Mediator.Send(new LoginRequest { Dto = dto });
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto dto)
        {
            var result = await Mediator.Send(new RegisterRequest { Dto = dto });
            return Ok(result);
        }
    }
}
