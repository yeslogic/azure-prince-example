using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PrinceApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrinceController : ControllerBase
    {
        private const string ExampleHtml = "<html><head><style type='text/css'>body { font-family: 'Inter' }</style></head><body>Hello from Prince</body></html>";
        private readonly IWebHostEnvironment _env;
        private readonly PrinceLogger _princeLogger;

        public PrinceController(ILogger<PrinceController> logger, IWebHostEnvironment env)
        {
            _env = env;
            _princeLogger = new PrinceLogger(logger);

        }

        [HttpGet]
        public FileStreamResult Get()
        {
            string princePath = Path.Combine(_env.ContentRootPath, @"prince\bin\prince.exe");
            string fontsCssPath = Path.Combine(_env.ContentRootPath, @"fonts\inter.css");
            Prince prn = new Prince(princePath, _princeLogger);
            prn.AddStyleSheet(fontsCssPath);
            Stream pdfOutput = new MemoryStream(10000);
            prn.ConvertString(ExampleHtml, pdfOutput);

            pdfOutput.Flush();
            pdfOutput.Position = 0;
            return File(pdfOutput, "application/pdf", "prince-demo.pdf");
        }
    }
}
