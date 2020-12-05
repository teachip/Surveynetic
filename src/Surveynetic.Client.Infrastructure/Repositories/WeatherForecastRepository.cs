using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Surveynetic.Client.Core.Interfaces.Repositories;
using Surveynetic.Shared.DTO;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Surveynetic.Client.Infrastructure.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IAccessTokenProvider _accessTokenProvider;

        public WeatherForecastRepository(HttpClient httpClient, IAccessTokenProvider accessTokenProvider)
        {
            _httpClient = httpClient;
            _accessTokenProvider = accessTokenProvider;
        }

        public async Task<WeatherForecastDto[]> GetAllAsync()
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "weatherforecast"))
            {
                var tokenResult = await _accessTokenProvider.RequestAccessToken();

                if (tokenResult.TryGetToken(out var token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
                    var response = await _httpClient.SendAsync(requestMessage);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadFromJsonAsync<WeatherForecastDto[]>();
                }
            }
            throw new NotImplementedException();
        }
    }
}
