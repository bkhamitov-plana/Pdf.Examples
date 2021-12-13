using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using PdfProvidersExamples.Helpers;
using PdfProvidersExamples.Models;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.Security;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace PdfProvidersExamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfSharpCoreController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPdf()
        {
            var document = new PdfDocument();
            var html = "<html><body style='color:green'><h1>PMKJ</h1></body></html>";
            PdfGenerator.AddPdfPages(document, html, PageSize.A4);

            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            var ms = new MemoryStream();
            document.Save(ms);

            return new FileStreamResult(ms, new MediaTypeHeaderValue("application/pdf"));
        }

        [HttpGet("{password}")]
        public IActionResult GetPdf(string password)
        {
            var document = new PdfDocument();
            var html = "<html><body style='color:green'><h1>PMKJ</h1></body></html>";
            PdfGenerator.AddPdfPages(document, html, PageSize.A4);

            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            #region security

            PdfSecuritySettings securitySettings = document.SecuritySettings;

            // Setting one of the passwords automatically sets the security level to
            // PdfDocumentSecurityLevel.Encrypted128Bit.
            securitySettings.UserPassword = password;
            securitySettings.OwnerPassword = password;
            // Don't use 40 bit encryption unless needed for compatibility
            //securitySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.Encrypted40Bit;

            // Restrict some rights.
            securitySettings.PermitAccessibilityExtractContent = false;
            securitySettings.PermitAnnotations = false;
            securitySettings.PermitAssembleDocument = false;
            securitySettings.PermitExtractContent = false;
            securitySettings.PermitFormsFill = true;
            securitySettings.PermitFullQualityPrint = false;
            securitySettings.PermitModifyDocument = true;
            securitySettings.PermitPrint = false;

            #endregion

            var ms = new MemoryStream();
            document.Save(ms);

            return new FileStreamResult(ms, new MediaTypeHeaderValue("application/pdf"));
        }

        [HttpGet("razor/{password}")]
        public IActionResult GetRazorPagePdf(string password)
        {
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

            var document = new PdfDocument();
            PdfGenerator.AddPdfPages(document, html, PageSize.A4);

            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            #region security

            PdfSecuritySettings securitySettings = document.SecuritySettings;

            // Setting one of the passwords automatically sets the security level to
            // PdfDocumentSecurityLevel.Encrypted128Bit.
            securitySettings.UserPassword = password;
            securitySettings.OwnerPassword = password;
            // Don't use 40 bit encryption unless needed for compatibility
            //securitySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.Encrypted40Bit;

            // Restrict some rights.
            securitySettings.PermitAccessibilityExtractContent = false;
            securitySettings.PermitAnnotations = false;
            securitySettings.PermitAssembleDocument = false;
            securitySettings.PermitExtractContent = false;
            securitySettings.PermitFormsFill = true;
            securitySettings.PermitFullQualityPrint = false;
            securitySettings.PermitModifyDocument = true;
            securitySettings.PermitPrint = false;

            #endregion

            var ms = new MemoryStream();
            document.Save(ms);

            return new FileStreamResult(ms, new MediaTypeHeaderValue("application/pdf"));
        }
    }
}
