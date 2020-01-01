using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace OptionsConfigure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //private readonly ILogger<WeatherForecastController> _logger;

        //private readonly IOptionsMonitor<MyOptions> _options;
        //public WeatherForecastController(IOptionsMonitor<MyOptions> options, ILogger<WeatherForecastController> logger)
        //{
        //    _options = options;
        //    _logger = logger;
        //}

        //[HttpGet]
        //public OkObjectResult Get() {
        //    _options.OnChange(_=>_logger.LogWarning(_options.CurrentValue.Name));
        //    return Ok(string.Format("Name:{0},Url:{1}", _options.CurrentValue.Name,_options.CurrentValue.Url));
        //}

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly MyOptions _options;
        public WeatherForecastController(IOptionsSnapshot<MyOptions> options, ILogger<WeatherForecastController> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        [HttpGet]
        public OkObjectResult Get()
        {
          //  _options.OnChange(_ => _logger.LogWarning(_options.CurrentValue.Name));
            return Ok(string.Format("Name:{0},Url:{1}", _options.Name, _options.Url));
        }
    }
}
