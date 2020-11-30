using Microsoft.AspNetCore.Components;
using Surveynetic.Client.Core.Interfaces.Repositories;
using Surveynetic.Shared.DTO;
using System.Threading.Tasks;

namespace Surveynetic.Client.Pages
{
    public partial class FetchData
    {
        [Inject] private IWeatherForecastRepository WeatherForecastRepository { get; set; }
        
        private WeatherForecastDto[] forecasts;

        protected override async Task OnInitializedAsync()
        {
            forecasts = await WeatherForecastRepository.GetAllAsync();
        }
    }
}
