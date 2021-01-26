using System.Collections.Generic;
using Newtonsoft.Json;

namespace G3.Services.OpenWeatherMap.Models
{
    public class OwmWeatherDetails
    {
        [JsonProperty("coord")]
        public OwmCoord Coords { get; set; }

        [JsonProperty("weather")]
        public List<OwmWeather> Weather { get; set; }
        
        [JsonProperty("base")]
        public string Base { get; set; }
        
        [JsonProperty("main")]
        public OwmMain Main { get; set; }
        
        [JsonProperty("visibility")]
        public int Visibility { get; set; }
        
        [JsonProperty("wind")]
        public OwmWind Wind { get; set; }
        
        [JsonProperty("clouds")]
        public OwmClouds Clouds { get; set; }
        
        [JsonProperty("dt")]
        public int Dt { get; set; }
        
        [JsonProperty("sys")]
        public OwmSys Sys { get; set; }
        
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("cod")]
        public int Cod { get; set; }
    }
}