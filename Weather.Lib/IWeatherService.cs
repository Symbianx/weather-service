using System;
using System.Threading.Tasks;

namespace Weather.Lib
{
    public interface IWeatherService
    {
        Task<WeatherResult> GetCurrentWeather(string city);
    }
}
