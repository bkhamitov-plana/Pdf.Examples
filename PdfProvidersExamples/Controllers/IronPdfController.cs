using Microsoft.AspNetCore.Mvc;
using PdfProvidersExamples.Helpers;
using PdfProvidersExamples.Models;
using System.Collections.Generic;
using System.IO;

namespace PdfProvidersExamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IronPdfController : ControllerBase
    {
        [HttpGet("razor/{password}")]
        public IActionResult PdfPassSave(string password)
        {
            IronPdf.Installation.TempFolderPath = $@"{Directory.GetCurrentDirectory()}";

            var html = RenderViewHelper.RenderPartialToString("Table.cshtml", new List<Person> {
                new Person
                {
                    Id = 1,
                    FirstName = "123",
                    LastName = "456",
                    MiddleName = "789",
                    PhoneNumber = "+77778985959"
                }
            });

            var ironPdfRender = new IronPdf.HtmlToPdf();
            var pdfDoc = ironPdfRender.RenderHtmlAsPdf(html);
            pdfDoc.Password = password;
            return File(pdfDoc.Stream.ToArray(), "application/pdf");
        }
    }
}
