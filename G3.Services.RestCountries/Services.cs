using G3.Core.Interfaces;
using G3.Services.RestCountries.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace G3.Services.RestCountries
{
    public static class Services
    {
        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ICountrySearch, RestCountrySearch>();
        }
    }
}