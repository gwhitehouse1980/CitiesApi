using System;
using System.Threading.Tasks;
using FluentAssertions;
using G3.Core.Interfaces;
using G3.Services.RestCountries.Implementations;
using G3.Services.RestCountries.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace G3.Services.RestCountries.Tests
{
    public class RestCountriesTests
    {
        private readonly ServiceProvider _serviceProvider;

        public RestCountriesTests()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            // Add settings
            serviceCollection.AddSingleton<RestCountriesSettings>(new RestCountriesSettings
            {
                AllCountriesUrl = "https://restcountries.eu/rest/v2/all",
                CountryUrl = "https://restcountries.eu/rest/v2/alpha/{0}"
            });
            
            // Add service implementations
            serviceCollection.AddTransient<ICountrySearch, RestCountrySearch>();
            serviceCollection.AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));
            
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }
        
        [Fact]
        public async Task CanGetListOfCountryCodes()
        {
            var countrySearch = _serviceProvider.GetService<ICountrySearch>();
            
            // Attempt to get a list of countries
            var countryCodes = await countrySearch.GetCountryCodes();
            
            // couple of tests on the returned data
            countryCodes.Should().NotBeNull();
            countryCodes.Should().HaveCountGreaterThan(0);
        }
        
        [Fact]
        public async Task ThrowsExceptionWithWrongUrl()
        {
            var settings = _serviceProvider.GetService<RestCountriesSettings>();
            settings.AllCountriesUrl = "some invalid url";
            
            var countrySearch = _serviceProvider.GetService<ICountrySearch>();
            
            // Attempt to get a list of countries
            await Assert.ThrowsAsync<UriFormatException>(countrySearch.GetCountryCodes);
        }
        
        [Fact]
        public async Task CanValidateCountryCodeAsTrue()
        {
            var countrySearch = _serviceProvider.GetService<ICountrySearch>();
            
            // Attempt to get a list of countries
            var countryCodes = await countrySearch.IsCountryCodeValid("USA");
            
            // couple of tests on the returned data
            countryCodes.Should().BeTrue();
        }
        
        [Fact]
        public async Task CanValidateCountryCodeAsFalse()
        {
            var countrySearch = _serviceProvider.GetService<ICountrySearch>();
            
            // Attempt to get a list of countries
            var countryCodes = await countrySearch.IsCountryCodeValid("XXX");
            
            // couple of tests on the returned data
            countryCodes.Should().BeFalse();
        }
        
        [Fact]
        public async Task CanGetCountryInformation()
        {
            var countrySearch = _serviceProvider.GetService<ICountrySearch>();
            
            // Attempt to get country information for the USA - Assumes the USA will still be there...
            var countryInformation = await countrySearch.GetCountryInformation("USA");
            
            // couple of tests on the returned data
            countryInformation.Should().NotBeNull();
            countryInformation.CountryName.Should().Be("United States of America");
            countryInformation.CountryCode.Should().Be("USA");
            countryInformation.ShortCountryCode.Should().Be("US");
            countryInformation.CurrencyCode.Should().Be("USD");
            countryInformation.CurrencySymbol.Should().Be("$");
        }
        
        [Fact]
        public async Task CountryInformationNullWithInvalildCode()
        {
            var countrySearch = _serviceProvider.GetService<ICountrySearch>();
            
            // Attempt to get a list of countries
            var countryInformation = await countrySearch.GetCountryInformation("XXX");
            
            // couple of tests on the returned data
            countryInformation.Should().BeNull();
        }
    }
}