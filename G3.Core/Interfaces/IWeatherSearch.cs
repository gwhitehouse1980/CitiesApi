using System.Threading.Tasks;
using G3.Core.Models;

namespace G3.Core.Interfaces
{
    /// <summary>
    /// The definition of what we expect a weather search to be. By defining it here
    ///  we can implement ANY service as a weather search, there is no dependency on a particular
    ///  service
    /// </summary>
    public interface IWeatherSearch
    {
        /// <summary>
        /// Get the current weather for a particular city
        /// </summary>
        /// <param name="cityName">The name of the city</param>
        /// <returns>A weather model containing weather info, or null if city is not found</returns>
        Task<WeatherModel> GetCurrentWeather(string cityName);
    }
}