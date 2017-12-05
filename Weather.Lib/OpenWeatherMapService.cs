using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Weather.Lib
{
    public class OpenWeatherMapService : IWeatherService
    {
        private const string OPEN_WEATHER_MAP_BASE_URL = "http://api.openweathermap.org/data/2.5/weather";
        private readonly string apiKey;

        public OpenWeatherMapService(string apiKey)
        {

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new System.ArgumentException("Must contain a value.", nameof(apiKey));
            }
            this.apiKey = apiKey;
        }

        public async Task<WeatherResult> GetCurrentWeather(string city)
        {
            using (HttpClient http = new HttpClient())
            {
                using (var response = await http.GetAsync($"{OPEN_WEATHER_MAP_BASE_URL}?units=metric&q={city}&appid={this.apiKey}"))
                {
                    response.EnsureSuccessStatusCode();
                    var responseContentString = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject<dynamic>(responseContentString);
                    return new WeatherResult
                    {
                        Maximum = result.main.temp_max,
                        Minimum = result.main.temp_min
                    };
                }
            }
        }
    }
}