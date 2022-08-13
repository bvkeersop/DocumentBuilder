using NDocument.Domain.Constants;
using NDocument.Domain.DocumentWriters;
using NDocument.Domain.Extensions;
using NDocument.Domain.Factories;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Model.Generic;
using NDocument.Domain.Options;
using NSubstitute;

namespace NDocument.Domain.Test.Unit.DocumentWriters
{
    [TestClass]
    public class HtmlDocumentWriterTests
    {
        [TestMethod]
        public async Task WriteToStreamAsync_OneConvertable_CallsWriteAsync()
        {
            // Arrange
            var options = new HtmlDocumentOptions();
            var memoryStream = new MemoryStream();
            var header1 = new Header1("header1");
            var HtmlStreamWriter = Substitute.For<IHtmlStreamWriter>();
            var HtmlConvertibles = new List<IHtmlConvertable>
            {
                header1
            };

            Func<Stream, HtmlDocumentOptions, IHtmlStreamWriter> factory
                = (Stream outputStream, HtmlDocumentOptions options) => HtmlStreamWriter;

            var htmlDocumentWriter = new HtmlDocumentWriter(factory, options);

            // Act
            await htmlDocumentWriter.WriteToStreamAsync(memoryStream, HtmlConvertibles);

            // Assert
            var header1Value = await header1.ToHtmlAsync(options, 2);
            await HtmlStreamWriter.Received(1).WriteLineAsync(HtmlIndicators.DocType);
            await HtmlStreamWriter.Received(1).WriteLineAsync(HtmlIndicators.Html.ToHtmlStartTag());
            await HtmlStreamWriter.Received(1).WriteLineAsync(HtmlIndicators.Body.ToHtmlStartTag(), 1);
            await HtmlStreamWriter.Received(1).WriteAsync(header1Value);
            await HtmlStreamWriter.Received(1).WriteLineAsync(HtmlIndicators.Body.ToHtmlEndTag(), 1);
            await HtmlStreamWriter.Received(1).WriteLineAsync(HtmlIndicators.Html.ToHtmlEndTag());
            await HtmlStreamWriter.Received(1).FlushAsync();
        }

        [TestMethod]
        public async Task WriteToStreamAsync_TwoConvertables_CallsWriteWriteAsyncTwice()
        {
            // Arrange
            var options = new HtmlDocumentOptions();
            var memoryStream = new MemoryStream();
            var header1 = new Header1("header1");
            var header2 = new Header2("header2");
            var HtmlStreamWriter = Substitute.For<IHtmlStreamWriter>();
            var HtmlConvertibles = new List<IHtmlConvertable>
            {
                header1,
                header2
            };

            Func<Stream, HtmlDocumentOptions, IHtmlStreamWriter> factory
                = (Stream outputStream, HtmlDocumentOptions options) => HtmlStreamWriter;

            var HtmlDocumentWriter = new HtmlDocumentWriter(factory, options);

            // Act
            await HtmlDocumentWriter.WriteToStreamAsync(memoryStream, HtmlConvertibles);

            // Assert
            var header1Value = await header1.ToHtmlAsync(options, 2);
            var header2Value = await header2.ToHtmlAsync(options, 2);
            await HtmlStreamWriter.Received(1).WriteLineAsync(HtmlIndicators.DocType);
            await HtmlStreamWriter.Received(1).WriteLineAsync(HtmlIndicators.Html.ToHtmlStartTag());
            await HtmlStreamWriter.Received(1).WriteLineAsync(HtmlIndicators.Body.ToHtmlStartTag(), 1);
            await HtmlStreamWriter.Received(1).WriteAsync(header1Value);
            await HtmlStreamWriter.Received(1).WriteAsync(header2Value);
            await HtmlStreamWriter.Received(1).WriteLineAsync(HtmlIndicators.Body.ToHtmlEndTag(), 1);
            await HtmlStreamWriter.Received(1).WriteLineAsync(HtmlIndicators.Html.ToHtmlEndTag());
            await HtmlStreamWriter.Received(1).FlushAsync();
        }
    }
}
