using Microsoft.AspNetCore.Components;
using Surveynetic.Client.Infrastructure.Repositories;
using Surveynetic.Shared.DTO;
using System.Threading.Tasks;

namespace Surveynetic.Client.Pages
{
    public partial class FetchData
    {
        [Inject] private WeatherForecastRepository weatherForecastRepository { get; set; }

        private WeatherForecastDto[] forecasts;

        protected override async Task OnInitializedAsync()
        {
            forecasts = await weatherForecastRepository.GetWeatherForecasts();
        }
    }
}
