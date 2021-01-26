using Newtonsoft.Json;

namespace G3.Services.OpenWeatherMap.Models
{
    public class OwmWeather
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}