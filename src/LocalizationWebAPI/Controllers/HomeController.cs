using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalizationWebAPI.Localize;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LocalizationWebAPI.Controllers
{
    [Route("{culture:culture}/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IStringLocalizer<Resource> localizer;
        public HomeController(IStringLocalizer<Resource> localizer)
        {
            this.localizer = localizer;
        }
        public string Get()
        {
            return localizer["Home"];
        }
    }
}