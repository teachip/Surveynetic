using Surveynetic.Shared.DTO;
using System.Threading.Tasks;

namespace Surveynetic.Client.Core.Interfaces
{
    public interface IWeatherForecastRepository
    {
        Task<WeatherForecastDto[]> GetAllAsync();
    }
}
