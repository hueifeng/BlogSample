using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace middleware.errorhandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static int i = 1;
        private readonly ICustomer _customer;
        public ValuesController(ICustomer customer) {
            this._customer = customer;
        }
        // GET api/values
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.ResponseCache(Duration = 259200)]
        public async Task<string> Get()
        {
            HttpResponseMessage results = new HttpResponseMessage(HttpStatusCode.OK);
           // var stream = new System.IO.MemoryStream(fileContent);

           //results.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain; charset=utf-8");
            results.Headers.CacheControl = new CacheControlHeaderValue
            {
                Public = true,
                MaxAge = TimeSpan.FromSeconds(259200)
            };
            results.Headers.Add("Cache-Control", "public, max-age=259200");
            results.Content = new StringContent($"{DateTime.Now}");
            return await results.Content.ReadAsStringAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return _customer.GetDate().ToString();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
