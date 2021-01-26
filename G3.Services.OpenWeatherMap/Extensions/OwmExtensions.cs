using Microsoft.Extensions.DependencyInjection;

namespace G3.Services.OpenWeatherMap.Extensions
{
    public static class OwmExtensions
    {
        public static IServiceCollection AddOpenWeatherMap(this IServiceCollection services)
        {
            OpenWeatherMap.Services.ConfigureServices(services);

            return services;
        }
    }
}