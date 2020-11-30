using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Surveynetic.Client.Core.Interfaces.Repositories;
using Surveynetic.Client.Core.Interfaces.Services;
using Surveynetic.Client.Infrastructure.Repositories;
using Surveynetic.Client.Infrastructure.Services;
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

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
                .AddScoped<ILocalStorageService, LocalStorageService>()
                .AddScoped<ICurrentUserService, CurrentUserService>();

            builder.Services.AddTransient<IWeatherForecastRepository, WeatherForecastRepository>();

            var build = builder.Build();

            var userService = build.Services.GetRequiredService<ICurrentUserService>();
            await userService.InitializeAsync();

            await build.RunAsync();
        }
    }
}
