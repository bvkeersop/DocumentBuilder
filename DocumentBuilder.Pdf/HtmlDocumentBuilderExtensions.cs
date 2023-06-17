using DocumentBuilder.DocumentBuilders;
using DocumentBuilder.Interfaces;
using DocumentBuilder.Model.Html;
using System.Text;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace DocumentBuilder.Pdf
{
    public static class HtmlDocumentBuilderExtensions
    {
        /// <summary>
        /// Writes the document to the provided output stream
        /// </summary>
        /// <param name="outputStream">The stream to write to</param>
        /// <returns><see cref="Task"/></returns>
        public static async Task BuildAsPdfAsync(this IHtmlDocumentBuilder htmlDocumentBuilder, Stream outputStream, Stream styleSheetStream)
        {
            _ = outputStream ?? throw new ArgumentNullException(nameof(outputStream));
            var styleSheet = GetStreamContents(styleSheetStream);
            var cssData = PdfGenerator.ParseStyleSheet(styleSheet);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var temporaryStream = new MemoryStream();
            await htmlDocumentBuilder.BuildAsync(temporaryStream);
            var html = GetStreamContents(temporaryStream);

            var pdf = PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4, 20, cssData);
            pdf.Save(outputStream);
        }

        public static string GetStreamContents(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using var streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }
    }
}