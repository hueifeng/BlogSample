using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Magicodes.ExporterAndImporter.Pdf;
using Magicodes.IE.Exporter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Magicodes.IE.Exporter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> ExporterExcel()
        {
            IExporter exporter = new ExcelExporter();

            var result = await exporter.Export(Path.Combine("wwwroot", "test.xlsx"), new List<StudentExcel>()
                {
                    new StudentExcel
                    {
                        Name = "MR.A",
                        Age = 18,
                        Remarks = "我叫MR.A,今年18岁",
                        Birthday=DateTime.Now
                    },
                    new StudentExcel
                    {
                        Name = "MR.B",
                        Age = 19,
                        Remarks = "我叫MR.B,今年19岁",
                        Birthday=DateTime.Now
                    },
                    new StudentExcel
                    {
                        Name = "MR.C",
                        Age = 20,
                        Remarks = "我叫MR.C,今年20岁",
                        Birthday=DateTime.Now
                    }
                });
            return File("test.xlsx", "application/ms-excel", result.FileName);
        }

        public async Task<IActionResult> ExporterPdf()
        {
           // IExporter exporter11 = new ExcelExporter();
            IExportListFileByTemplate exporter = new PdfExporter();
            var result = await exporter.ExportListByTemplate(Path.Combine("wwwroot", "test.pdf"), new List<StudentPdf>()
            {
                 new StudentPdf
                    {
                        Name = "MR.A",
                        Age = 18,
                        Remarks = "我叫MR.A,今年18岁",
                        Birthday=DateTime.Now
                    },
                    new StudentPdf
                    {
                        Name = "MR.B",
                        Age = 19,
                        Remarks = "我叫MR.B,今年19岁",
                        Birthday=DateTime.Now
                    },
                    new StudentPdf
                    {
                        Name = "MR.C",
                        Age = 20,
                        Remarks = "我叫MR.C,今年20岁",
                        Birthday=DateTime.Now
                    }
            });
            return File("test.pdf", "application/pdf", result.FileName);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
