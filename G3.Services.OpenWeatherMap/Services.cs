using G3.Core.Interfaces;
using G3.Services.OpenWeatherMap.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace G3.Services.OpenWeatherMap
{
    public static class Services
    {
        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IWeatherSearch, OwmWeatherSearch>();
        }
    }
}