using IronPdf;
using System;
using System.IO;

namespace IronPDFExample
{
    public class Program
    {
        public static string csprojPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public static string viewsFolder = "views";
        public static string outputFolder = "output";
        public static string ownerPassword = "owner123";
        public static string password = "123";

        public static void Main(string[] args)
        {
            GenerateEncryptedPdfFromHtmlString();
            GenerateEncryptedPdfFromHtmlFile();
        }

        public static void GenerateEncryptedPdfFromHtmlString()
        {
            var pdfFilename = "pdfFromHtmlStringEncrypted.pdf";
            var outputFilePath = Path.Combine(csprojPath, outputFolder, pdfFilename);

            Console.WriteLine($"Generating and saving PDF-file From Html String pdf to: {outputFilePath}");
            var htmlText = "<h1>Hello World!</h1><br><p>I'm just created.</p>";
            var pdfDoc = new HtmlToPdf().RenderHtmlAsPdf(htmlText);
            pdfDoc.Password = password;
            pdfDoc.OwnerPassword= ownerPassword;
            pdfDoc.SaveAs(outputFilePath);
            Console.WriteLine($"Pdf has been saved.\r\n\r\n");
        }

        public static void GenerateEncryptedPdfFromHtmlFile()
        {
            var htmlFilename = "index.html";
            var sourceHtmlFilePath = Path.Combine(csprojPath, viewsFolder, htmlFilename);
            var htmlText = File.ReadAllText(sourceHtmlFilePath);

            var pdfFilename = "pdfFromHtmlFileEncrypted.pdf";
            var outputFilePath = Path.Combine(csprojPath, outputFolder, pdfFilename);

            Console.WriteLine($"Generating and saving PDF-file From Html File pdf to: {outputFilePath}");
            var pdfDoc = new HtmlToPdf().RenderHtmlAsPdf(htmlText);
            pdfDoc.Password = password;
            pdfDoc.OwnerPassword = ownerPassword;
            pdfDoc.SaveAs(outputFilePath);
            Console.WriteLine($"Pdf has been saved.\r\n\r\n");
        }
    }
}
