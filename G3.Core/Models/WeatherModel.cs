namespace G3.Core.Models
{
    public class WeatherModel
    {
        public double Temperature { get; set; }
        
        public double TemperatureMin { get; set; }
        
        public double TemperatureMax { get; set; }
        
        public string Description { get; set; }
        
        public int Humidity { get; set; }
        
        public int Pressure { get; set; }
        
        public double WindSpeed { get; set; }
    }
}