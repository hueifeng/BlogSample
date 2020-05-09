using System;
using System.Collections.Generic;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Magicodes.ExporterAndImporter.Html;
using Magicodes.ExporterAndImporter.Pdf;
using Magicodes.ExporterAndImporter.Tests.Models.Export;
using Magicodes.ExporterAndImporter.Word;

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
        public async Task HandleSuccessfulReqeustAsync(HttpContext context, object body, int httpStatusCode, Type type)
        {
            #region  excel
            //var dt = ToDataTable(body?.ToString());
            ////Excel
            //IExporter exporter = new ExcelExporter();
            //var result = await exporter.ExportAsByteArray(dt, type);
            //context.Response.Headers.Add("Content-Disposition", "attachment;filename=test.xlsx");
            //context.Response.ContentType = "application/vnd.ms-excel; charset=UTF-8";
            #endregion

            #region PDF
            //IExportFileByTemplate exporter = new PdfExporter();
            //var tplPath = Path.Combine(Directory.GetCurrentDirectory(), "ExportTemplates",
            //    "batchReceipt.cshtml");
            //var tpl = File.ReadAllText(tplPath);
            //var obj = JsonConvert.DeserializeObject(body.ToString(), type);
            //var result = await exporter.ExportBytesByTemplate(obj, tpl, type);
            //context.Response.Headers.Add("Content-Disposition", "attachment;filename=test.pdf");
            //context.Response.ContentType = "application/pdf; charset=UTF-8";
            #endregion

            #region HTML
            //IExportFileByTemplate exporter = new HtmlExporter();
            //var tplPath = Path.Combine(Directory.GetCurrentDirectory(), "ExportTemplates",
            //    "receipt.cshtml");
            //var tpl = File.ReadAllText(tplPath);
            //var obj = JsonConvert.DeserializeObject(body.ToString(), type);
            //var result = await exporter.ExportBytesByTemplate(obj, tpl, type);
            //context.Response.Headers.Add("Content-Disposition", "attachment;filename=test.html");
            //context.Response.ContentType = "application/html; charset=UTF-8";
            #endregion

            #region Word
            IExportFileByTemplate exporter = new WordExporter();
            var tplPath = Path.Combine(Directory.GetCurrentDirectory(), "ExportTemplates",
                "receipt.cshtml");
            var tpl = File.ReadAllText(tplPath);
            var obj = JsonConvert.DeserializeObject(body.ToString(), type);
            var result = await exporter.ExportBytesByTemplate(obj, tpl, type);
            context.Response.Headers.Add("Content-Disposition", "attachment;filename=test.docx");
            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

            #endregion

            await context.Response.Body.WriteAsync(result, 0, result.Length);
        }
    }
}
