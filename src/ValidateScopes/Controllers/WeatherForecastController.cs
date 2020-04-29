using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ValidateScopes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly SingletonDependency _singletonDependency;

       // private readonly ScopedDependency _scopedDependency;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            SingletonDependency singletonDependency
          //  ,ScopedDependency scopedDependency
           )
        {
            _logger = logger;
            this._singletonDependency = singletonDependency;
           // this._scopedDependency = scopedDependency;
        }

        [HttpGet]
        public int Get()
        {
            return _singletonDependency.GetNextCounter();
            // var rng = new Random();
            // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //     {
            //         Date = DateTime.Now.AddDays(index),
            //         TemperatureC = rng.Next(-20, 55),
            //         Summary = Summaries[rng.Next(Summaries.Length)]
            //     })
            //     .ToArray();
        }
    }
}