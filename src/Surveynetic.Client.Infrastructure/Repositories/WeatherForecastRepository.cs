using Surveynetic.Shared.DTO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Surveynetic.Client.Infrastructure.Repositories
{
    public class WeatherForecastRepository
    {
        private readonly HttpClient _httpClient;

        public WeatherForecastRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherForecastDto[]> GetWeatherForecasts()
        {
            var response = await _httpClient.GetAsync("https://localhost:44320/WeatherForecast");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WeatherForecastDto[]>(json);
        }
    }
}
