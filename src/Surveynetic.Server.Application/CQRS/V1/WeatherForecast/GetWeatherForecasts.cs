using MediatR;
using Surveynetic.Shared.DTO;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Surveynetic.Server.Application.CQRS.V1.WeatherForecast
{
    public class GetWeatherForecasts : IRequest<WeatherForecastDto[]>
    {
        public class GetWeatherForecastsHandler : IRequestHandler<GetWeatherForecasts, WeatherForecastDto[]>
        {
            public GetWeatherForecastsHandler()
            {
            }

            private static readonly string[] Summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            public async Task<WeatherForecastDto[]> Handle(GetWeatherForecasts request, CancellationToken cancellationToken = default)
            {
                var rng = new Random();
                var data = Enumerable.Range(1, 5).Select(index => new WeatherForecastDto
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
                return data;
            }
        }
    }
}
