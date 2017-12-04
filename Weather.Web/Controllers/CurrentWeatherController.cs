using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherLib;

namespace Weather.Web.Controllers
{
    [Route("api/[controller]")]
    public class CurrentWeatherController : Controller
    {
        private readonly IWeatherService weatherService;
        private readonly ILogger<CurrentWeatherController> logger;

        public CurrentWeatherController(IWeatherService weatherService, ILogger<CurrentWeatherController> logger)
        {
            this.weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET api/values/5
        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            var weatherResult = await weatherService.GetCurrentWeather(city);
            logger.LogDebug($"Min: {weatherResult.Minimum}, Max: {weatherResult.Maximum}");
            return Ok(weatherResult);
        }
    }
}
