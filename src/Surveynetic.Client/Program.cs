using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Surveynetic.Client.Core.Interfaces;
using Surveynetic.Client.Infrastructure.Repositories;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Surveynetic.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddTransient<IWeatherForecastRepository, WeatherForecastRepository>();

            await builder.Build().RunAsync();
        }
    }
}
