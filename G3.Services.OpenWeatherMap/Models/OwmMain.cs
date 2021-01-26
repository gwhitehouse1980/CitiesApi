using Newtonsoft.Json;

namespace G3.Services.OpenWeatherMap.Models
{
    public class OwmMain
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("pressure")]
        public int Pressure { get; set; }
        
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
        
        [JsonProperty("temp_min")]
        public double TemperatureMin { get; set; }
        
        [JsonProperty("temp_max")]
        public double TemperatureMax { get; set; }
    }
}