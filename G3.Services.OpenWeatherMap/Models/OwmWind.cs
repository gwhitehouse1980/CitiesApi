using Newtonsoft.Json;

namespace G3.Services.OpenWeatherMap.Models
{
    public class OwmWind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public int Direction { get; set; }
    }
}