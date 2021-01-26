using System;
using System.Collections.Generic;
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
    [Route("countries")]
    [Authorize]
    public class CountryController : Controller
    {
        private readonly ICountrySearch _countrySearch;
        private readonly ILogger<CitiesController> _logger;

        public CountryController(ICountrySearch countrySearch, ILogger<CitiesController> logger)
        {
            _countrySearch = countrySearch;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("search/{countryCode}")]
        [SwaggerParameter("ApiKey", "Api authentication key", ParameterLocation.Query)]
        public async Task<IActionResult> Search(string countryCode)
        {
            _logger.LogTrace("Search started");

            CountryModel result;
            
            try
            {
                result = await _countrySearch.GetCountryInformation(countryCode);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception when searching for Country {countryCode}", ex);
                return BadRequest(ex.Message);
            }
            
            return Ok(result);
        }
        
        [HttpGet]
        [Route("codes")]
        [SwaggerParameter("ApiKey", "Api authentication key", ParameterLocation.Query)]
        public async Task<IActionResult> Codes()
        {
            _logger.LogTrace("Codes started");

            List<CountryCodeModel> result;
            
            try
            {
                result = await _countrySearch.GetCountryCodes();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception when getting Country Codes", ex);
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }
    }
}
