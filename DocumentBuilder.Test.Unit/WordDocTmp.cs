using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentBuilder.Test.Unit
{
    [TestClass]
    public class WordDocTmp
    {
        [TestMethod]
        public void Test()
        {
            CreateWordDocFromTemplate("C:\\Users\\bartv\\source\\repos\\DocumentBuilder\\Document.docx", "C:\\Users\\bartv\\source\\repos\\DocumentBuilder\\Output.docx");
            AddTitleToDocument("C:\\Users\\bartv\\source\\repos\\DocumentBuilder\\Output.docx", "TestTitle");
        }

        public void CreateWordDocFromTemplate(string templatePath, string outputPath)
        {
            // Load the template document
            using (WordprocessingDocument templateDoc = WordprocessingDocument.Open(templatePath, false))
            {
                // Create a new document based on the template
                WordprocessingDocument newDoc = WordprocessingDocument.Create(outputPath, WordprocessingDocumentType.Document);

                // Add a main document part to the new document
                MainDocumentPart mainPart = newDoc.AddMainDocumentPart();

                // Create a new document element with the same body as the template document
                Document newDocElement = new Document();
                newDocElement.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
                newDocElement.Body = new Body();

                // Set the new document element as the document for the new document part
                mainPart.Document = newDocElement;

                // Add the style definitions from the template document to the new document part
                StyleDefinitionsPart styleDefsPart = templateDoc.MainDocumentPart.StyleDefinitionsPart;
                if (styleDefsPart != null)
                {
                    mainPart.DeletePart(mainPart.StyleDefinitionsPart);
                    mainPart.AddPart(styleDefsPart);
                }

                // Save the new document
                mainPart.Document.Save();
                newDoc.Close();
            }
        }

        public void AddTitleToDocument(string documentPath, string title)
        {
            // Open the document
            using (WordprocessingDocument doc = WordprocessingDocument.Open(documentPath, true))
            {
                // Get the main document part
                MainDocumentPart mainPart = doc.MainDocumentPart;

                // Get the title style from the document
                StyleDefinitionsPart styleDefsPart = mainPart.StyleDefinitionsPart;
                Style titleStyle = styleDefsPart.Styles.Elements<Style>().FirstOrDefault(s => s.StyleId.Value.Equals("Title"));

                // Create a new paragraph with the title text
                Paragraph titleParagraph = new Paragraph();
                Run titleRun = new Run(new Text(title));
                titleParagraph.Append(titleRun);

                // Apply the title style to the paragraph
                titleParagraph.ParagraphProperties = new ParagraphProperties(new ParagraphStyleId() { Val = "Title" });

                // Insert the title paragraph at the beginning of the document
                Body body = mainPart.Document.Body;
                body.InsertAt(titleParagraph, 0);

                // Save the changes to the document
                mainPart.Document.Save();
            }
        }
    }
}
