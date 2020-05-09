using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Threading.Tasks;

namespace Magicodes._64.Filters
{
    public class MagicodesFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var obj = (ObjectResult)context.Result;
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            var json = JsonConvert.SerializeObject(obj.Value, timeConverter);
            var type = obj.Value.GetType().GenericTypeArguments[0];
            await new Extensions().HandleSuccessfulReqeustAsync(context.HttpContext, json, 200, type);
        }

    }
}
