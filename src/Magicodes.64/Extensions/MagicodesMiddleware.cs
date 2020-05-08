using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Magicodes._64.Extensions
{
    public class MagicodesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MagicodesMiddleware> _logger;
        private IActionResultExecutor<ObjectResult> _executor { get; }
        private static readonly ActionDescriptor _emptyActionDescriptor = new ActionDescriptor();
        public MagicodesMiddleware(RequestDelegate next, ILogger<MagicodesMiddleware> logger, IActionResultExecutor<ObjectResult> executor)
        {
            _next = next;
            _logger = logger;
            _executor = executor;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using var memoryStream = new MemoryStream();
            var originalResponseBodyStream = context.Response.Body;
            try
            {
                context.Response.Body = memoryStream;
                await _next.Invoke(context);
                context.Response.Body = originalResponseBodyStream;

                var bodyAsText = await ReadResponseBodyStreamAsync(memoryStream);
                var result = new ObjectResult(bodyAsText) {
                    StatusCode = 200
                };
                var routeData = context.GetRouteData();
                var actionContext = new ActionContext(context, routeData, _emptyActionDescriptor);

                result.ContentTypes.Add("application/json");
                await _executor.ExecuteAsync(actionContext, result);
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        public async Task<string> ReadResponseBodyStreamAsync(Stream bodyStream)
        {
            bodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(bodyStream).ReadToEndAsync();
            bodyStream.Seek(0, SeekOrigin.Begin);

            return responseBody;
        }
    }
}
