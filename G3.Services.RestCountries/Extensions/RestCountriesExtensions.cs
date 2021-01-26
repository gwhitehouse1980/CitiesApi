using Microsoft.Extensions.DependencyInjection;

namespace G3.Services.RestCountries.Extensions
{
    public static class RestCountriesExtensions
    {
        public static IServiceCollection AddRestCountries(this IServiceCollection services)
        {
            Services.ConfigureServices(services);

            return services;
        }
    }
}