using Newtonsoft.Json;

namespace G3.Services.OpenWeatherMap.Models
{
    public class OwmCoord
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }
}