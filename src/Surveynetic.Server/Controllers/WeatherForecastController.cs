using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Surveynetic.Server.Application.CQRS.V1.WeatherForecast;
using System.Threading.Tasks;

namespace Surveynetic.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetWeatherForecasts()));
        }
    }
}
