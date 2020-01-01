using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetAspectCore.Service;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAspectCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICustomService _icustomserveice;
        public ValuesController(ICustomService icustomService) {
            this._icustomserveice = icustomService;
        }

        // GET api/values
        [HttpGet]
        public DateTime Get()
        {
            Test.Val = "hahahahahah";
            return _icustomserveice.GetDateTime();
        }

    }
}
