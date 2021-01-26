using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using G3.Api.Cities.Swagger;
using G3.Core.Enums;
using G3.Core.Interfaces;
using G3.Core.Models;
using G3.Modules.Cities.Entities;
using G3.Modules.Cities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace G3.Api.Cities.Controllers
{
    [Route("cities")]
    [Authorize]
    public class CitiesController : Controller
    {
        private readonly IRepository<City, CityModel> _cityRepository;
        private readonly IEntitySearch<City, CityListModel> _entitySearch;
        private readonly ILogger<CitiesController> _logger;

        public CitiesController(IRepository<City, CityModel> cityRepository, IEntitySearch<City, CityListModel> entitySearch, ILogger<CitiesController> logger)
        {
            _cityRepository = cityRepository;
            _entitySearch = entitySearch;
            _logger = logger;
        }
        
        [HttpPost]
        [Route("add")]
        [SwaggerParameter("ApiKey", "Api authentication key", ParameterLocation.Query)]
        public async Task<IActionResult> Add([FromBody] CityModel model)
        {
            _logger.LogTrace("Add started");

            RepositoryActionResultModel<CityModel> result;
            
            try
            {
                result = await _cityRepository.Add(model);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception when adding City", ex);
                return BadRequest(ex.Message);
            }
            
            return _actionToResult(result);
        }
        
        [HttpGet]
        [Route("get/{id}")]
        [SwaggerParameter("ApiKey", "Api authentication key", ParameterLocation.Query)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogTrace("Get started");

            RepositoryActionResultModel<CityModel> result;
            
            try
            {
                result = await _cityRepository.Get(id);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception when getting City", ex);
                return BadRequest(ex.Message);
            }

            return _actionToResult(result);
        }
        
        [HttpPost]
        [Route("update/{id}")]
        [SwaggerParameter("ApiKey", "Api authentication key", ParameterLocation.Query)]
        public async Task<IActionResult> Update(Guid id, [FromBody] CityModel model)
        {
            _logger.LogTrace("Update started");

            RepositoryActionResultModel<CityModel> result;
            
            try
            {
                result = await _cityRepository.Update(id, model);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception when updating City", ex);
                return BadRequest(ex.Message);
            }

            return _actionToResult(result);
        }
        
        [HttpGet]
        [Route("delete/{id}")]
        [SwaggerParameter("ApiKey", "Api authentication key", ParameterLocation.Query)]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogTrace("Delete started");

            RepositoryActionResultModel<CityModel> result;
            
            try
            {
                result = await _cityRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception when updating City", ex);
                return BadRequest(ex.Message);
            }

            return _actionToResult(result);
        }
        
        [HttpPost]
        [Route("search")]
        [SwaggerParameter("ApiKey", "Api authentication key", ParameterLocation.Query)]
        public async Task<IActionResult> Search(Guid id, [FromBody] List<SearchFilterModel> searchFilters)
        {
            _logger.LogTrace("Search started");

            List<CityListModel> result;
            
            try
            {
                result = await _entitySearch.GetSearchResults(searchFilters);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception when searching for Cities", ex);
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        private IActionResult _actionToResult(RepositoryActionResultModel<CityModel> theResult)
        {
            // Dependent on the return type, we will return either BadRequest or OK
            if (theResult.ResultType == ResultTypeEnum.NotFound)
            {
                return NotFound(theResult);
            } 
            if (theResult.ResultType == ResultTypeEnum.FailedValidation || theResult.ResultType == ResultTypeEnum.FailedVerification)
            {
                return BadRequest(theResult);
            }

            return Ok(theResult);
        }
    }
}
