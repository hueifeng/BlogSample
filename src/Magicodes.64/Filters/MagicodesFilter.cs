using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Threading.Tasks;
using Magicodes._64.Attributes;
using Microsoft.AspNetCore.Http;

namespace Magicodes._64.Filters
{
    public class MagicodesFilter : IAsyncResultFilter
    {
        private readonly Extensions _extensions;
        public MagicodesFilter()
        {
            _extensions=new Extensions();
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result;
            if (result is ObjectResult objectResult)
            {
                var endpoint = context.HttpContext.GetEndpoint();
                var endpointMagicodesData = endpoint?.Metadata.GetMetadata<IMagicodesData>();
                if (endpointMagicodesData != null)
                {
                    var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
                    var json = JsonConvert.SerializeObject(objectResult.Value, timeConverter);
                    await _extensions.HandleSuccessfulReqeustAsync(context: context.HttpContext, body: json, httpStatusCode: 200,
                        type: endpointMagicodesData.Type);
                }
            }
            else
            {
                await next();
            }
        }

    }
}
