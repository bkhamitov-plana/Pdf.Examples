using System.Collections.Generic;
using System.IO;
using PdfProvidersExamples.Models;
using RazorEngineCore;

namespace PdfProvidersExamples.Helpers
{
    public static class RenderViewHelper
    {
        private static string viewsPath = Path.Combine(Directory.GetCurrentDirectory(), "Templates");

        public static string RenderPartialToString<T>(string viewPath, T model)
        {
            string viewAbsolutePath = Path.Combine(viewsPath, viewPath);
            var razorPageContent = File.ReadAllText(viewAbsolutePath, System.Text.Encoding.UTF8);
            IRazorEngine razorEngine = new RazorEngine();
            var template = razorEngine.Compile<RazorEngineTemplateBase<T>>(razorPageContent);
            string result = template.Run(instance =>
            {
                instance.Model = model;
            });
            return result;
        }

    }
}
