using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using G3.Core.Interfaces;
using G3.Core.Models;
using G3.Services.RestCountries.Models;
using G3.Services.RestCountries.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace G3.Services.RestCountries.Implementations
{
    /// <summary>
    /// Country search implementation for use with RestCountries api v2
    /// </summary>
    public class RestCountrySearch : ICountrySearch
    {
        private readonly RestCountriesSettings _settings;
        private readonly ILogger<RestCountrySearch> _logger;

        public RestCountrySearch(IOptions<RestCountriesSettings> settings, ILogger<RestCountrySearch> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }
        
        public RestCountrySearch(RestCountriesSettings settings, ILogger<RestCountrySearch> logger)
        {
            _settings = settings;
            _logger = logger;
        }
        
        /// <summary>
        /// Gets a list of country codes
        /// </summary>
        /// <returns>A list of country codes with their names</returns>
        public async Task<List<CountryCodeModel>> GetCountryCodes()
        {
            List<CountryCodeModel> result;
            
            try
            {
                // Call the api function to load data from the endpoint
                var apiData = await _callApiUrl<List<RestCountryModel>>(_settings.AllCountriesUrl);
                
                // Now we can look at this data and return the required content
                result = apiData.Select(o => new CountryCodeModel
                {
                    CountryCode = o.Alpha3Code,
                    CountryName = o.Name
                }).ToList();
            }
            catch (Exception ee)
            {
                _logger.LogCritical($"Exception when in call to GetCountryCodes", ee);
                
                // Throw exception back
                throw;
            }

            return result;
        }

        /// <summary>
        /// Validates if a country code is valid
        /// </summary>
        /// <param name="countryCode">The three digit country code</param>
        /// <returns>A oolean value indicating if the code is valid</returns>
        public async Task<bool> IsCountryCodeValid(string countryCode)
        {
            bool result;

            try
            {
                // Validate the country code is the correct length
                if (countryCode.Length != 3)
                {
                    return false;
                }

                // Call the api function to load data from the endpoint
                var apiData =
                    await _callApiUrl<RestCountryModel>(string.Format(_settings.CountryUrl, countryCode.ToUpper()));

                // Now we can look at this data and return the required content
                result = apiData != null;
            }
            catch (WebException we)
            {
                // Validate against the code not being found
                if ((we.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }

                throw;
            }
            catch (Exception ee)
            {
                _logger.LogCritical($"Exception when in call to IsCountryCodeValid", ee);
                
                // Throw exception back
                throw;
            }

            return result;
        }

        /// <summary>
        /// Gets country information based n a country code
        /// </summary>
        /// <param name="countryCode">The three digit country code</param>
        /// <returns>A model containing our required country information</returns>
        public async Task<CountryModel> GetCountryInformation(string countryCode)
        {
            CountryModel result;

            try
            {
                // Validate the country code is the correct length
                if (countryCode.Length != 3)
                {
                    return null;
                }

                // Call the api function to load data from the endpoint
                var apiData =
                    await _callApiUrl<RestCountryModel>(string.Format(_settings.CountryUrl, countryCode.ToUpper()));

                // No data, or something went wrong
                if (apiData == null)
                {
                    return null;
                }
                
                // Now we can look at this data and return the required content
                result = new CountryModel
                {
                    CountryName = apiData.Name,
                    CountryCode = apiData.Alpha3Code,
                    ShortCountryCode = apiData.Alpha2Code,
                    CurrencyCode = apiData.Currencies.FirstOrDefault()?.Code ?? "Unknown",
                    CurrencySymbol = apiData.Currencies.FirstOrDefault()?.Symbol ?? "Unknown"
                };
            }
            catch (WebException we)
            {
                // Validate against the code not being found
                if ((we.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw;
            }
            catch (Exception ee)
            {
                _logger.LogCritical($"Exception when in call to GetCountryInformation", ee);
                
                // Throw exception back
                throw;
            }

            return result;
        }

        private async Task<TResult> _callApiUrl<TResult>(string apiUrl)
        {
            TResult result;
            
            try
            {
                // Create a new request based on the url generated above
                var request = WebRequest.Create(apiUrl);

                // Get the response
                var response = await request.GetResponseAsync();

                // Convert to a stream reader
                using (var sr = new StreamReader(response.GetResponseStream() ?? throw new Exception("Null response stream")))
                {
                    // Read all content for converting into objects
                    var jsonContent = await sr.ReadToEndAsync();

                    // Convert into objects
                    result = JsonConvert.DeserializeObject<TResult>(jsonContent);
                }
            }
            catch (Exception ee)
            {
                _logger.LogCritical($"Exception when calling rest countries utl {apiUrl}", ee);
                throw;
            }

            return result;
        }
    }
}