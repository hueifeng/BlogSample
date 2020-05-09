using Magicodes._64.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Magicodes._64.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        [Magicodes(Type = typeof(StudentExcel))]
        public List<StudentExcel> Get()
        {
            var list = new List<StudentExcel>
            {
                new StudentExcel
                {
                    Name = "MR.A",
                    Age = 18,
                    Remarks = "我叫MR.A,今年18岁",
                    Birthday = DateTime.Now
                },
                new StudentExcel
                {
                    Name = "MR.B",
                    Age = 19,
                    Remarks = "我叫MR.B,今年19岁",
                    Birthday = DateTime.Now
                },
                new StudentExcel
                {
                    Name = "MR.C",
                    Age = 20,
                    Remarks = "我叫MR.C,今年20岁",
                    Birthday = DateTime.Now
                }
            };
            return list;
        }

        [HttpGet("Test")]
        [Magicodes]
        public ContentResult Test()
        {
            return Content("1111");
            //var list = new List<StudentExcel>
            //{
            //    new StudentExcel
            //    {
            //        Name = "MR.A",
            //        Age = 18,
            //        Remarks = "我叫MR.A,今年18岁",
            //        Birthday = DateTime.Now
            //    },
            //    new StudentExcel
            //    {
            //        Name = "MR.B",
            //        Age = 19,
            //        Remarks = "我叫MR.B,今年19岁",
            //        Birthday = DateTime.Now
            //    },
            //    new StudentExcel
            //    {
            //        Name = "MR.C",
            //        Age = 20,
            //        Remarks = "我叫MR.C,今年20岁",
            //        Birthday = DateTime.Now
            //    }
            //};
            //return list;
        }
    }
}
