using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using System.Collections.Generic;
using System.IO;

namespace PdfSharpCoreExample.ConsoleApp
{
    public class Program
    {

        public static void Main(string[] args)
        {
            PdfDocument doc = new PdfDocument();

            doc.Info.Title = "Table example";

            // Page Options
            PdfPage pdfPage = doc.AddPage();
            pdfPage.Height = 842;//842
            pdfPage.Width = 590;

            // Get an XGraphics object for drawing
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);


            // Text format
            XStringFormat format = new XStringFormat();
            format.LineAlignment = XLineAlignment.Near;
            format.Alignment = XStringAlignment.Near;
            var tf = new XTextFormatter(graph);

            var people = new List<Person>
            {
                new Person
                {
                    Id = 1,
                    FirstName = "FirstName 1",
                    LastName = "LastName 1",
                    MiddleName = "MiddleName 1",
                    PhoneNumber = "PhoneNumber 1"
                },
                new Person
                {
                    Id = 2,
                    FirstName = "FirstName 2",
                    LastName = "LastName 2",
                    MiddleName = "MiddleName 2",
                    PhoneNumber = "PhoneNumber 2"
                },
                new Person
                {
                    Id = 3,
                    FirstName = "FirstName 3",
                    LastName = "LastName 3",
                    MiddleName = "MiddleName 3",
                    PhoneNumber = "PhoneNumber 3"
                }
            };

            XFont fontParagraph = new XFont("Verdana", 8, XFontStyle.Regular);

            // Row elements
            int rectangleWidth = 80;
            int rectangleHeight = 20;

            // page structure options
            double lineHeight = 20;
            int marginLeft = 20;
            int marginTop = 20;
            double marginLeftOffset = 1.5;
            double marginTopOffset = 1.25;

            XSolidBrush headerBrush = new XSolidBrush(XColors.DarkGreen);
            XSolidBrush rowBrush = new XSolidBrush(XColors.LightGray);

            graph.DrawRectangle(headerBrush, marginLeft, marginTop, pdfPage.Width - 2 * marginLeft, rectangleHeight);

            var headerColumns = new string[]
            {
                "Id",
                "LastName",
                "FirstName",
                "MiddleName",
                "Phone Number"
            };

            for(int i = 0; i < headerColumns.Length; i++)
            {
                var columnMarginLeft = (marginLeft * marginLeftOffset) * (i + 1) + rectangleWidth * i;
                var columnMarginTop = (marginTop * marginTopOffset);
                tf.DrawString(headerColumns[i], fontParagraph, XBrushes.White,
                              new XRect(columnMarginLeft, columnMarginTop, rectangleWidth, rectangleHeight), format);
            }


            for (var i = 0; i < people.Count; i++)
            {
                var columnMarginTop = rectangleHeight * (i + 2);

                graph.DrawRectangle(rowBrush, marginLeft, columnMarginTop, rectangleWidth + marginLeft, rectangleHeight);
                double idMarginLeft = (marginLeft * marginLeftOffset);
                double idMarginTop = ((rectangleHeight) * (i + 1)) + (marginTop * marginTopOffset);
                tf.DrawString(
                    people[i].Id.ToString(),
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(idMarginLeft, idMarginTop, rectangleWidth, rectangleHeight),
                    format);

                var lastNameColumnMarginLeft = (marginLeft + rectangleWidth);

                graph.DrawRectangle(rowBrush, lastNameColumnMarginLeft, columnMarginTop, rectangleWidth + marginLeft, rectangleHeight);
                double lastNameMarginLeft = (marginLeft * marginLeftOffset);
                double lastNameMarginTop = ((rectangleHeight) * (i + 1)) + (marginTop * marginTopOffset);
                tf.DrawString(
                    people[i].LastName.ToString(),
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(lastNameMarginLeft, lastNameMarginTop, rectangleWidth, rectangleHeight),
                    format);
            }

            double CalcDrawStringMarginLeft()
            {
                return (marginLeft * marginLeftOffset);
            }

            double CalcDrawStringMarginTop()
            {
                return (marginLeft * marginLeftOffset);
            }

            //tf.DrawString("Id", fontParagraph, XBrushes.White,
            //              new XRect(marginLeft * marginLeftOffset, marginTop + marginTopOffset, rectangleWidth, rectangleHeight), format);

            //// stampo il primo elemento insieme all'header
            //graph.DrawRectangle(rect_style1, marginLeft, dist_Y2 + marginTop, el1_width, rect_height);
            //tf.DrawString("text1", fontParagraph, XBrushes.Black,
            //              new XRect(marginLeft, dist_Y + marginTop, el1_width, el_height), format);

            ////ELEMENT 2 - BIG 380
            //graph.DrawRectangle(rect_style1, marginLeft + offSetX_1 + interLine_X_1, dist_Y2 + marginTop, el2_width, rect_height);
            //tf.DrawString(
            //    "text2",
            //    fontParagraph,
            //    XBrushes.Black,
            //    new XRect(marginLeft + offSetX_1 + interLine_X_1, dist_Y + marginTop, el2_width, el_height),
            //    format);


            ////ELEMENT 3 - SMALL 80

            //graph.DrawRectangle(rect_style1, marginLeft + offSetX_2 + interLine_X_2, dist_Y2 + marginTop, el1_width, rect_height);
            //tf.DrawString(
            //    "text3",
            //    fontParagraph,
            //    XBrushes.Black,
            //    new XRect(marginLeft + offSetX_2 + 2 * interLine_X_2, dist_Y + marginTop, el1_width, el_height),
            //    format);

            //for (var p = 0; p < people.Count; p++)
            //{
            //    dist_Y = lineHeight * (p+1 + 1);
            //    dist_Y2 = dist_Y - 2;
            //    //if (i % 2 == 1)
            //    //{
            //    //  graph.DrawRectangle(TextBackgroundBrush, marginLeft, lineY - 2 + marginTop, pdfPage.Width - marginLeft - marginRight, lineHeight - 2);
            //    //}

            //    //ELEMENT 1 - SMALL 80
            //    graph.DrawRectangle(rect_style1, marginLeft, marginTop + dist_Y2, 30, rect_height);
            //    tf.DrawString(
            //        people[p].Id.ToString(),
            //        fontParagraph,
            //        XBrushes.Black,
            //        new XRect(marginLeft, marginTop + dist_Y, el1_width, el_height),
            //        format);
            //}

            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var outputFilename = Path.Combine(path, "output", "test1.pdf");
            doc.Save(outputFilename);

            //byte[] bytes = null;
            //using (MemoryStream stream = new MemoryStream())
            //{
            //    document.Save(stream, true);
            //    bytes = stream.ToArray();
            //}

            //SendFileToResponse(bytes, "HelloWorld_test.pdf");
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
