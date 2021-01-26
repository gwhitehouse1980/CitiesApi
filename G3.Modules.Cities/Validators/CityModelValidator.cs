using System.Threading.Tasks;
using G3.Core.Interfaces;
using G3.Core.Models;
using G3.Modules.Cities.Models;

namespace G3.Modules.Cities.Validators
{
    /// <summary>
    /// IValildator implementation for validating a new or updated city entry
    /// </summary>
    public class CityModelValidator : IValidator<CityModel>
    {
        private readonly ICountrySearch _countrySearch;
        private const int NameMaxLength = 50;
        private const int StateMaxLength = 50;

        public CityModelValidator(ICountrySearch countrySearch)
        {
            _countrySearch = countrySearch;
        }
        
        public async Task<ValidationResultModel> ValidateAdd(CityModel model)
        {
            var result = new ValidationResultModel();
            
            // Simple validation of string length and such like, assumes 
            //  we are not using standard MVC model validation

            if (model.Name == null)
            {
                // Failed as it is required
                result.AddMessage("Name", $"Value is required");
            }
            
            if (model.Name?.Length > NameMaxLength)
            {
                // Failed as it is too long
                result.AddMessage("Name", $"Maximum length of {NameMaxLength}");
            }
            
            if (model.State == null)
            {
                // Failed as it is required
                result.AddMessage("State", $"Value is required");
            }
            
            if (model.State?.Length > StateMaxLength)
            {
                // Failed as it is too long
                result.AddMessage("State", $"Maximum length of {StateMaxLength}");
            }
            
            if (model.CountryCode == null)
            {
                // Failed as it is required
                result.AddMessage("CountryCode", $"Value is required");
            }
            
            if (model.CountryCode?.Length != 3)
            {
                // Failed as it is too long
                result.AddMessage("CountryCode", $"Invalid CountryCode, must be three characters");
            } else if (!await _countrySearch.IsCountryCodeValid(model.CountryCode))
            {
                // Failed as not found in datasource
                result.AddMessage("CountryCode", $"Invalid CountryCode, not found on database");
            }
            
            if (model.TouristRating.HasValue && (model.TouristRating.Value < 0 || model.TouristRating.Value > 5))
            {
                // Failed as it is required
                result.AddMessage("TouristRating", $"Value is invalid, must be a number between 1 and 5");
            }
            
            return await Task.FromResult(result);
        }

        public async Task<ValidationResultModel> ValidateUpdate(CityModel model)
        {
            return await ValidateAdd(model);
        }
    }
}