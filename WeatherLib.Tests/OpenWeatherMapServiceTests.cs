using System;
using System.Threading.Tasks;
using Xunit;

namespace WeatherLib.Tests
{
    public class OpenWeatherMapServiceTests
    {
        [Fact]
        public async Task GetCurrentWeather_Lisbon()
        {
            var service = new OpenWeatherMapService("948f7c5035a11f66bf2347f57aa3fe4d");
            var weather = await service.GetCurrentWeather("Lisbon");

            Assert.NotNull(weather);
        }
    }
}
