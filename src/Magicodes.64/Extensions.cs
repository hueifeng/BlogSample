using System;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace Magicodes._64
{
    public class Extensions
    {
        public async Task<string> ReadResponseBodyStreamAsync(Stream bodyStream)
        {
            bodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(bodyStream).ReadToEndAsync();
            bodyStream.Seek(0, SeekOrigin.Begin);
            return responseBody;
        }
        public static DataTable ToDataTable(string json)
        {
            return JsonConvert.DeserializeObject<DataTable>(json);
        }
        public async Task HandleSuccessfulReqeustAsync(HttpContext context,object body,int httpStatusCode,Type type)
        {
            //var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            //var json = JsonConvert.SerializeObject(body, timeConverter);
            var dt = ToDataTable(body?.ToString());
            //var t = body.GetType().GenericTypeArguments[0];
            //Excel
            IExporter exporter = new ExcelExporter();
            var result = await exporter.ExportAsByteArray(dt, type);
            context.Response.Headers.Add("Content-Disposition", "attachment;filename=test.xlsx");
            context.Response.ContentType = "application/vnd.ms-excel; charset=UTF-8";
            //context.Response.Headers.Add("Content-Type", "application/vnd.ms-excel; charset=UTF-8");
            await context.Response.Body.WriteAsync(result, 0, result.Length);
        }
    }
}
