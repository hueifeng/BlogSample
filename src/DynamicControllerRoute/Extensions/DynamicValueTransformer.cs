using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace DynamicControllerRoute.Extensions
{
    public class DynamicValueTransformer : DynamicRouteValueTransformer
    {
        public override async ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext,
            RouteValueDictionary values)
        {
            values["controller"] = "WeatherForecast";
            values["action"] = values["anything"];
            return values;
        }
    }
}
