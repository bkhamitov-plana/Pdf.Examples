using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfProvidersExamples.Helpers;
using PdfProvidersExamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PdfProvidersExamples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RazorEngineController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var result = RenderViewHelper.RenderPartialToString("Table.cshtml", new List<Person> {
                new Person
                {
                    Id = 1,
                    FirstName = "123",
                    LastName = "456",
                    MiddleName = "789",
                    PhoneNumber = "+77778985959"
                }
            });

            return new ContentResult {
                ContentType = "text/html",
                Content = result
            };
        }
    }
}
