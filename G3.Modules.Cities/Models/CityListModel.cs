using System;
using G3.Core.Enums;
using G3.Core.Interfaces;
using G3.Core.Models;

namespace G3.Modules.Cities.Models
{
    public class CityListModel : IModel
    {
        // Interface
        public Guid? Id { get; set; }
        public EntityStatusEnum EntityStatus { get; set; }
        public string Name { get; set; }

        // Custom
        public string State { get; set; }
        public string CountryCode { get; set; }
        public int? TouristRating { get; set; }
        public DateTime? DateEstablished { get; set; }
        public int? EstimatedPopulation { get; set; }
        
        // From country lookup
        public string ShortCountryCode { get; set; }
        public string CountryName { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        
        // From weather lookup
        public WeatherModel Weather { get; set; }
    }
}