using ceTe.DynamicPDF;
using pdfPgEls = ceTe.DynamicPDF.PageElements;
using System.IO;
using ceTe.DynamicPDF.Cryptography;

namespace DynamicPDF.Core.SuiteExample.ConsoleApp
{
    public class Program
    {
        public static string csprojPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public static string viewsFolder = "views";
        public static string outputFolder = "output";
        public static string ownerPassword = "owner";
        public static string userPassword = "user";

        static void Main(string[] args)
        {
            CreateSimpleEncryptedPdf();
            CreateTableEncryptedPdf();
        }

        public static void CreateSimpleEncryptedPdf()
        {
            Document doc = new Document();

            Page page = new Page();
            doc.Pages.Add(page);
            page.Elements.Add(new pdfPgEls.Label("Hello World!", 0, 0, 100, 12, Font.Helvetica, 12));

            Aes256Security security = new Aes256Security(ownerPassword, userPassword);
            doc.Security = security;

            var outputFilename = Path.Combine(csprojPath, outputFolder, "simpleEncrypted.pdf");
            doc.Draw(outputFilename);
        }

        public static void CreateTableEncryptedPdf()
        {
            // Create a PDF Document 
            Document doc = new Document();

            // Create a Page and add it to the document 
            Page page = new Page();
            doc.Pages.Add(page);

            var table = new pdfPgEls.Table2(0, 0, 600, 600);
            table.CellDefault.Border.Color = RgbColor.Blue;
            table.CellSpacing = 5.0f;

            // Add columns to the table
            table.Columns.Add(150);
            table.Columns.Add(90);
            table.Columns.Add(90);

            // Add rows to the table and add cells to the rows
            pdfPgEls.Row2 row1 = table.Rows.Add(40, Font.HelveticaBold, 16, RgbColor.Black,
                RgbColor.Gray);
            row1.CellDefault.Align = TextAlign.Center;
            row1.CellDefault.VAlign = VAlign.Center;
            row1.Cells.Add("Header 1");
            row1.Cells.Add("Header 2");
            row1.Cells.Add("Header 3");

            pdfPgEls.Row2 row2 = table.Rows.Add(30);
            pdfPgEls.Cell2 cell1 = row2.Cells.Add("Rowheader 1", Font.HelveticaBold, 16,
                RgbColor.Black, RgbColor.Gray, 1);
            cell1.Align = TextAlign.Center;
            cell1.VAlign = VAlign.Center;
            row2.Cells.Add("Item 1");
            row2.Cells.Add("Item 2, this item is much longer than the rest so that " +
                "you can see that each row will automatically expand to fit to the " +
                "height of the largest element in that row.");

            pdfPgEls.Row2 row3 = table.Rows.Add(30);
            pdfPgEls.Cell2 cell2 = row3.Cells.Add("Rowheader 2", Font.HelveticaBold, 16,
                RgbColor.Black, RgbColor.Gray, 1);
            cell2.Align = TextAlign.Center;
            cell2.VAlign = VAlign.Center;
            row3.Cells.Add("Item 3");
            row3.Cells.Add("Item 4");

            // Add the table to the page
            page.Elements.Add(table);

            Aes256Security security = new Aes256Security(ownerPassword, userPassword);
            doc.Security = security;

            var outputFilename = Path.Combine(csprojPath, outputFolder, "tableEncrypted.pdf");
            doc.Draw(outputFilename);
        }
    }
}
