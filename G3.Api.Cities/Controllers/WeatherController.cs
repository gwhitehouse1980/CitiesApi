using System;
using System.Threading.Tasks;
using G3.Api.Cities.Swagger;
using G3.Core.Interfaces;
using G3.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace G3.Api.Cities.Controllers
{
    [Route("weather")]
    [Authorize]
    public class WeatherController : Controller
    {
        private readonly IWeatherSearch _weatherSearch;
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(IWeatherSearch weatherSearch, ILogger<WeatherController> logger)
        {
            _weatherSearch = weatherSearch;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("search/{cityName}")]
        [SwaggerParameter("ApiKey", "Api authentication key", ParameterLocation.Query)]
        public async Task<IActionResult> Search(string cityName)
        {
            _logger.LogTrace("Search started");

            WeatherModel result;
            
            try
            {
                result = await _weatherSearch.GetCurrentWeather(cityName);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception when getting weather for City {cityName}", ex);
                return BadRequest(ex.Message);
            }
            
            return Ok(result);
        }
    }
}
