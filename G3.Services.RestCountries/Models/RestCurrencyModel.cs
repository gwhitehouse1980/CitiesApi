using Newtonsoft.Json;

namespace G3.Services.RestCountries.Models
{
    public class RestCurrencyModel
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}