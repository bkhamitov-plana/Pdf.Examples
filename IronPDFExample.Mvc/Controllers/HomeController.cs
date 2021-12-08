using IronPDFExample.Mvc.Extensions;
using IronPDFExample.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;

namespace IronPDFExample.Mvc.Controllers
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
            return View(new List<Person> {
                new Person
                {
                    Id = 1,
                    FirstName = "123",
                    LastName = "456",
                    MiddleName = "789",
                    PhoneNumber = "+77778985959"
                }
            });
        }

        public IActionResult Pdf()
        {
            IronPdf.Installation.TempFolderPath = $@"{Program.csprojPath}/IronPDFExample.Mvc/Views/Home";
            //IronPdf.Installation.LinuxAndDockerDependenciesAutoConfig = true;
            var people = new List<Person> {
                new Person
                {
                    Id = 1,
                    FirstName = "123",
                    LastName = "456",
                    MiddleName = "789",
                    PhoneNumber = "+77778985959"
                }
            };
            var html = this.RenderViewAsync("Index", people);
            var ironPdfRender = new IronPdf.HtmlToPdf();
            var pdfDoc = ironPdfRender.RenderHtmlAsPdf(html.Result);
            return File(pdfDoc.Stream.ToArray(), "application/pdf");
        }

        public IActionResult PdfPass()
        {
            IronPdf.Installation.TempFolderPath = $@"{Program.csprojPath}/IronPDFExample.Mvc/Views/Home";
            //IronPdf.Installation.LinuxAndDockerDependenciesAutoConfig = true;
            var people = new List<Person> {
                new Person
                {
                    Id = 1,
                    FirstName = "123",
                    LastName = "456",
                    MiddleName = "789",
                    PhoneNumber = "+77778985959"
                }
            };
            var html = this.RenderViewAsync("Index", people);
            var ironPdfRender = new IronPdf.HtmlToPdf();
            var pdfDoc = ironPdfRender.RenderHtmlAsPdf(html.Result);
            pdfDoc.Password = "123";
            return File(pdfDoc.Stream.ToArray(), "application/pdf");
        }

        public IActionResult PdfSave()
        {
            IronPdf.Installation.TempFolderPath = $@"{Program.csprojPath}/IronPDFExample.Mvc/Views/Home";
            //IronPdf.Installation.LinuxAndDockerDependenciesAutoConfig = true;
            var people = new List<Person> {
                new Person
                {
                    Id = 1,
                    FirstName = "123",
                    LastName = "456",
                    MiddleName = "789",
                    PhoneNumber = "+77778985959"
                }
            };
            var html = this.RenderViewAsync("Index", people);
            var ironPdfRender = new IronPdf.HtmlToPdf();
            var pdfDoc = ironPdfRender.RenderHtmlAsPdf(html.Result);
            pdfDoc.SaveAs($@"{Program.csprojPath}/IronPDFExample.Mvc/output/table.pdf");
            return NoContent();
        }

        public IActionResult PdfPassSave()
        {
            IronPdf.Installation.TempFolderPath = $@"{Program.csprojPath}/IronPDFExample.Mvc/Views/Home";
            //IronPdf.Installation.LinuxAndDockerDependenciesAutoConfig = true;
            var people = new List<Person> {
                new Person
                {
                    Id = 1,
                    FirstName = "123",
                    LastName = "456",
                    MiddleName = "789",
                    PhoneNumber = "+77778985959"
                }
            };
            var html = this.RenderViewAsync("Index", people);
            var ironPdfRender = new IronPdf.HtmlToPdf();
            var pdfDoc = ironPdfRender.RenderHtmlAsPdf(html.Result);
            pdfDoc.Password = "123";
            pdfDoc.SaveAs($@"{Program.csprojPath}/IronPDFExample.Mvc/output/password.pdf");
            return NoContent();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
