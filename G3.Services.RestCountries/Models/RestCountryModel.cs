using System.Collections.Generic;
using Newtonsoft.Json;

namespace G3.Services.RestCountries.Models
{
    public class RestCountryModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("topLevelDomain")]
        public List<string> TopLevelDomain { get; set; }
        
        [JsonProperty("alpha2Code")]
        public string Alpha2Code { get; set; }
        
        [JsonProperty("alpha3Code")]
        public string Alpha3Code { get; set; }
        
        [JsonProperty("callingCodes")]
        public List<string> CallingCodes { get; set; }
        
        [JsonProperty("capital")]
        public string Capital { get; set; }
        
        [JsonProperty("altSpellings")]
        public List<object> AltSpellings { get; set; }
        
        [JsonProperty("region")]
        public string Region { get; set; }
        
        [JsonProperty("subregion")]
        public string Subregion { get; set; }
        
        [JsonProperty("population")]
        public int Population { get; set; }
        
        [JsonProperty("latlng")]
        public List<object> Latlng { get; set; }
        
        [JsonProperty("demonym")]
        public string Demonym { get; set; }
        
        [JsonProperty("area")]
        public double? Area { get; set; }
        
        [JsonProperty("gini")]
        public double? Gini { get; set; }
        
        [JsonProperty("timezones")]
        public List<string> Timezones { get; set; }
        
        [JsonProperty("borders")]
        public List<object> Borders { get; set; }
        
        [JsonProperty("nativeName")]
        public string NativeName { get; set; }
        
        [JsonProperty("numericCode")]
        public string NumericCode { get; set; }
        
        [JsonProperty("currencies")]
        public List<RestCurrencyModel> Currencies { get; set; }
        
        [JsonProperty("languages")]
        public List<RestLanguageModel> Languages { get; set; }
        
        [JsonProperty("translations")]
        public RestTranslationModel Translations { get; set; }
        
        [JsonProperty("flag")]
        public string Flag { get; set; }
        
        [JsonProperty("regionalBlocs")]
        public List<object> RegionalBlocs { get; set; }
        
        [JsonProperty("cioc")]
        public string Cioc { get; set; }
    }
}