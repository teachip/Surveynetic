using System;
using System.Text.Json.Serialization;

namespace Surveynetic.Shared.DTO
{
    public class WeatherForecastDto
    {
        [JsonPropertyName("date")] public DateTime Date { get; set; }

        [JsonPropertyName("temperature_c")] public int TemperatureC { get; set; }

        [JsonPropertyName("summary")] public string Summary { get; set; }

        [JsonIgnore] public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
