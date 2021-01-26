using System.Collections.Generic;
using System.Threading.Tasks;
using G3.Core.Models;

namespace G3.Core.Interfaces
{
    /// <summary>
    /// The definition of what we expect a country search to be. By defining it here
    ///  we can implement ANY service as a country search, there is no dependency on a particular
    ///  service
    /// </summary>
    public interface ICountrySearch
    {
        /// <summary>
        /// Gets a list of country codes
        /// </summary>
        /// <returns>A list of country codes with their names</returns>
        Task<List<CountryCodeModel>> GetCountryCodes();
        
        /// <summary>
        /// Validates if a country code is valid
        /// </summary>
        /// <param name="countryCode">The three digit country code</param>
        /// <returns>A oolean value indicating if the code is valid</returns>
        Task<bool> IsCountryCodeValid(string countryCode);
        
        /// <summary>
        /// Gets country information based n a country code
        /// </summary>
        /// <param name="countryCode">The three digit country code</param>
        /// <returns>A model containing our required country information</returns>
        Task<CountryModel> GetCountryInformation(string countryCode);
    }
}