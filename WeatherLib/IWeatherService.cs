using System;
using System.Threading.Tasks;

namespace WeatherLib
{
    public interface IWeatherService
    {
        Task<WeatherResult> GetCurrentWeather(string city);
    }
}
