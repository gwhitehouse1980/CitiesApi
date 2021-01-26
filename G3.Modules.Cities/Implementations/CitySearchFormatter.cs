using System.Threading.Tasks;
using G3.Core.Interfaces;
using G3.Modules.Cities.Models;

namespace G3.Modules.Cities.Implementations
{
    /// <summary>
    /// City search formatter, allows for adding additional properties to search results
    /// </summary>
    public class CitySearchFormatter : IEntitySearchFormatter<CityListModel>
    {
        private readonly ICountrySearch _countrySearch;
        private readonly IWeatherSearch _weatherSearch;

        public CitySearchFormatter(ICountrySearch countrySearch, IWeatherSearch weatherSearch)
        {
            _countrySearch = countrySearch;
            _weatherSearch = weatherSearch;
        }
        
        public async Task<CityListModel> FormatModel(CityListModel newModel)
        {
            // Lookup the country information using country code
            var countryInformation = await _countrySearch.GetCountryInformation(newModel.CountryCode);

            // Now update details on the output model
            if (countryInformation != null)
            {
                newModel.CurrencyCode = countryInformation.CurrencyCode;
                newModel.CurrencySymbol = countryInformation.CurrencySymbol;
                newModel.CountryName = countryInformation.CountryName;
                newModel.ShortCountryCode = countryInformation.ShortCountryCode;
            }
            
            // Get the weather for this city, this could return null when API usage has run out
            newModel.Weather = await _weatherSearch.GetCurrentWeather(newModel.Name); 

            return newModel;
        }
    }
}