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
        private readonly ILogger<PrinceController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly string princePath;
        private readonly PrinceLogger princeLogger;
        private readonly string fontsCssPath;

        public PrinceController(ILogger<PrinceController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
            princePath = Path.Combine(_env.ContentRootPath, @"prince\bin\prince.exe");
            princeLogger = new PrinceLogger(_logger);
            fontsCssPath = Path.Combine(_env.ContentRootPath, @"fonts\inter.css");
        }

        [HttpGet]
        public FileStreamResult Get()
        {
            Prince prn = new Prince(princePath, princeLogger);
            prn.AddStyleSheet(fontsCssPath);
            Stream pdfOutput = new MemoryStream(10000);
            prn.ConvertString(ExampleHtml, pdfOutput);

            pdfOutput.Flush();
            pdfOutput.Position = 0;
            return File(pdfOutput, "application/pdf", "prince-demo.pdf");
        }
    }
}
