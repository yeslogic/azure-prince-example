using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PrinceApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrinceController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<PrinceController> _logger;
        private readonly IWebHostEnvironment _env;

        public PrinceController(ILogger<PrinceController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        [HttpGet]
        public FileStreamResult Get()
        {
            string path = Path.Combine(_env.ContentRootPath, @"prince-20200812-win64\bin\prince.exe");
            string fontsCssPath = Path.Combine(_env.ContentRootPath, @"fonts\inter.css");
            Prince prn = new Prince(path);
            prn.AddStyleSheet(fontsCssPath);
            Stream pdfOutput = new MemoryStream(10000);
            prn.ConvertString("<html><head><style type='text/css'>body { font-family: 'Inter' }</style></head><body>Hello from Prince</body></html>", pdfOutput);
            //byte[] fileContent = pdfOutput.ReadAllBytes();

            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
            pdfOutput.Flush();
            pdfOutput.Position = 0;
            return File(pdfOutput, "application/pdf", "prince-demo.pdf");
        }
    }
}
