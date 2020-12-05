using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Surveynetic.Server.Application.CQRS.V1.WeatherForecast;
using System.Threading.Tasks;

namespace Surveynetic.Server.Controllers
{
    [Authorize]
    public class WeatherForecastController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await Mediator.Send(new GetWeatherForecasts()));
        }
    }
}
