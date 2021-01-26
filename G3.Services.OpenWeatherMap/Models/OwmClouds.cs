using Newtonsoft.Json;

namespace G3.Services.OpenWeatherMap.Models
{
    public class OwmClouds
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }
}