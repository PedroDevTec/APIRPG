using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet("{number}")]
        public IActionResult Get(int number)
        {
            var rng = new Random();
            var weatherData = Enumerable.Range(1, number)
                .Select(index => new
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55)
                })
                .ToArray();

            if (number < 5)
            {
                return Ok("Você foi pro Abismo.");
            }

            return Ok(weatherData);
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Lógica do microserviço
            var rng = new Random();
            var weatherData = new[]
            {
                new { Date = DateTime.Now.AddDays(1), TemperatureC = rng.Next(-20, 55) },
                new { Date = DateTime.Now.AddDays(2), TemperatureC = rng.Next(-20, 55) },
                new { Date = DateTime.Now.AddDays(3), TemperatureC = rng.Next(-20, 55) },
                new { Date = DateTime.Now.AddDays(4), TemperatureC = rng.Next(-20, 55) },
                new { Date = DateTime.Now.AddDays(5), TemperatureC = rng.Next(-20, 55) }
            };

            return Ok(weatherData);
        }
    }
}