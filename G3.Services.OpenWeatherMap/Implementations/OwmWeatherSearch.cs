using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using G3.Core.Interfaces;
using G3.Core.Models;
using G3.Services.OpenWeatherMap.Models;
using G3.Services.OpenWeatherMap.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace G3.Services.OpenWeatherMap.Implementations
{
    /// <summary>
    /// Weather search implementation for use with Open Weather Map api
    /// </summary>
    public class OwmWeatherSearch : IWeatherSearch
    {
        private readonly OwmSettings _settings;
        private readonly ILogger<OwmWeatherSearch> _logger;

        public OwmWeatherSearch(IOptions<OwmSettings> settings, ILogger<OwmWeatherSearch> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }
        
        public OwmWeatherSearch(OwmSettings settings, ILogger<OwmWeatherSearch> logger)
        {
            _settings = settings;
            _logger = logger;
        }
        
        /// <summary>
        /// Get the current weather for a particular city
        /// </summary>
        /// <param name="cityName">The name of the city</param>
        /// <returns>A weather model containing weather info, or null if city is not found</returns>
        public async Task<WeatherModel> GetCurrentWeather(string cityName)
        {
            WeatherModel result;

            try
            {
                // Call the api function to load data from the endpoint
                var apiData =
                    await _callApiUrl<OwmWeatherDetails>(string.Format(_settings.CitySearchUrl, HttpUtility.UrlEncode(cityName), _settings.ApiKey));

                // No data, or something went wrong
                if (apiData == null)
                {
                    return null;
                }
                
                // Now we can look at this data and return the required content
                result = new WeatherModel
                {
                    Temperature = apiData.Main.Temperature,
                    TemperatureMin = apiData.Main.TemperatureMin,
                    TemperatureMax = apiData.Main.TemperatureMin,
                    Humidity = apiData.Main.Humidity,
                    Pressure = apiData.Main.Pressure,
                    Description = apiData.Weather.FirstOrDefault()?.Description,
                    WindSpeed = apiData.Wind.Speed
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