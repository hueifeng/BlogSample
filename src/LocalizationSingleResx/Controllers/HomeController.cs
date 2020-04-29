using LocalizationSingleResx.Localize;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LocalizationSingleResx.Controllers
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