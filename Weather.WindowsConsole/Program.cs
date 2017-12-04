using System;
using System.Threading.Tasks;
using WeatherLib;

namespace Weather.WindowsConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var weatherService = new OpenWeatherMapService("948f7c5035a11f66bf2347f57aa3fe4d");

            var results = await Task.WhenAll(
                weatherService.GetCurrentWeather("Lisbon"),
                weatherService.GetCurrentWeather("Leiria"));

            Console.WriteLine($"Weather in Lisbon: {results[0].Minimum}ºC to {results[0].Maximum}ºC");
            Console.WriteLine($"Weather in Leiria: {results[1].Minimum}ºC to {results[1].Maximum}ºC");
        }
    }
}
