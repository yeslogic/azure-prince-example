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
            string path = Path.Combine(_env.ContentRootPath, @"prince\bin\prince.exe");
            string fontsCssPath = Path.Combine(_env.ContentRootPath, @"fonts\inter.css");
            Prince prn = new Prince(path);
            prn.AddStyleSheet(fontsCssPath);
            Stream pdfOutput = new MemoryStream(10000);
            prn.ConvertString("<html><head><style type='text/css'>body { font-family: 'Inter' }</style></head><body>Hello from Prince</body></html>", pdfOutput);

            pdfOutput.Flush();
            pdfOutput.Position = 0;
            return File(pdfOutput, "application/pdf", "prince-demo.pdf");
        }
    }
}
