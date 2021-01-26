using System.Threading.Tasks;
using FluentAssertions;
using G3.Core.Interfaces;
using G3.Services.OpenWeatherMap.Implementations;
using G3.Services.OpenWeatherMap.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace G3.Services.OpenWeatherMap.Tests
{
    public class OpenWeatherMapTests
    {
        private readonly ServiceProvider _serviceProvider;

        public OpenWeatherMapTests()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            // Add settings
            serviceCollection.AddSingleton<OwmSettings>(new OwmSettings
            {
                CitySearchUrl = "http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&APPID={1}",
                ApiKey = "ab562e6d2ee636c3ee8b3264d03de7a3"
            });

            // Add service implementations
            serviceCollection.AddTransient<IWeatherSearch, OwmWeatherSearch>();
            serviceCollection.AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));
            
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }
        
        [Fact]
        public async Task CanGetWeatherForCity()
        {
            var weatherSearch = _serviceProvider.GetService<IWeatherSearch>();
            
            // Attempt to get a list of countries
            var weatherDetails = await weatherSearch.GetCurrentWeather("Milton Keynes");
            
            // couple of tests on the returned data
            weatherDetails.Should().NotBeNull();
        }
        
        [Fact]
        public async Task GetWeatherForUnknownCityReturnsNull()
        {
            var weatherSearch = _serviceProvider.GetService<IWeatherSearch>();
            
            // Attempt to get a list of countries
            var weatherDetails = await weatherSearch.GetCurrentWeather("Some Unknown City");
            
            // couple of tests on the returned data
            weatherDetails.Should().BeNull();
        }
    }
}