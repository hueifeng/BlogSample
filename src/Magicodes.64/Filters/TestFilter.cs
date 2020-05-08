using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;
using System.Threading.Tasks;

namespace Magicodes._64.Filters
{
    public class TestFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            //TODO 获取自动Attribute是否存在
    
        
            var obj = (ObjectResult)context.Result;
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            var json = JsonConvert.SerializeObject(obj.Value, timeConverter);
            var dt = ToDataTable(json);
            var t = obj.Value.GetType().GenericTypeArguments[0];
            //Excel
            IExporter exporter = new ExcelExporter();
            var result = await exporter.ExportAsByteArray(dt, t);
            context.HttpContext.Response.Headers.Add("Content-Disposition", "attachment;filename=test.xls");
            context.HttpContext.Response.Headers.Add("Content-Type", "application/vnd.ms-excel; charset=UTF-8");
            await context.HttpContext.Response.Body.WriteAsync(result, 0, result.Length);
            //Csv
            //IExporter exporter = new CsvExporter();
            //var result = await exporter.ExportAsByteArray(dt, t);
            //context.HttpContext.Response.Headers.Add("Content-Disposition", "attachment;filename=test.csv");
            //context.HttpContext.Response.Headers.Add("Content-Type", " text/csv; charset=UTF-8");
            //await context.HttpContext.Response.Body.WriteAsync(result, 0, result.Length);

        }

        public static DataTable ToDataTable(string json)
        {
            return JsonConvert.DeserializeObject<DataTable>(json);
        }
    }
}
