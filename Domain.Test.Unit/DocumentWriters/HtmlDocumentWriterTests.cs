using DocumentBuilder.Domain.Constants;
using DocumentBuilder.Domain.Extensions;
using DocumentBuilder.Domain.Interfaces;
using DocumentBuilder.Domain.Options;
using DocumentBuilder.Domain.DocumentWriters;
using DocumentBuilder.Domain.Model.Generic;
using NSubstitute;

namespace DocumentBuilder.Domain.Test.Unit.DocumentWriters
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
            var htmlStreamWriterMock = Substitute.For<IHtmlStreamWriter>();
            var htmlConvertibles = new List<IHtmlConvertable>
            {
                header1
            };

            Func<Stream, HtmlDocumentOptions, IHtmlStreamWriter> factory
                = (outputStream, options) => htmlStreamWriterMock;

            var htmlDocumentWriter = new HtmlDocumentWriter(factory, options);

            // Act
            await htmlDocumentWriter.WriteToStreamAsync(memoryStream, htmlConvertibles);

            // Assert
            var header1Value = await header1.ToHtmlAsync(options, 2);
            await htmlStreamWriterMock.Received(1).WriteLineAsync(HtmlIndicators.DocType);
            await htmlStreamWriterMock.Received(1).WriteLineAsync(HtmlIndicators.Html.ToHtmlStartTag());
            await htmlStreamWriterMock.Received(1).WriteLineAsync(HtmlIndicators.Body.ToHtmlStartTag(), 1);
            await htmlStreamWriterMock.Received(1).WriteAsync(header1Value);
            await htmlStreamWriterMock.Received(1).WriteLineAsync(HtmlIndicators.Body.ToHtmlEndTag(), 1);
            await htmlStreamWriterMock.Received(1).WriteLineAsync(HtmlIndicators.Html.ToHtmlEndTag());
            await htmlStreamWriterMock.Received(1).FlushAsync();
        }

        [TestMethod]
        public async Task WriteToStreamAsync_TwoConvertables_CallsWriteWriteAsyncTwice()
        {
            // Arrange
            var options = new HtmlDocumentOptions();
            var memoryStream = new MemoryStream();
            var header1 = new Header1("header1");
            var header2 = new Header2("header2");
            var htmlStreamWriterMock = Substitute.For<IHtmlStreamWriter>();
            var htmlConvertibles = new List<IHtmlConvertable>
            {
                header1,
                header2
            };

            Func<Stream, HtmlDocumentOptions, IHtmlStreamWriter> factory
                = (outputStream, options) => htmlStreamWriterMock;

            var HtmlDocumentWriter = new HtmlDocumentWriter(factory, options);

            // Act
            await HtmlDocumentWriter.WriteToStreamAsync(memoryStream, htmlConvertibles);

            // Assert
            var header1Value = await header1.ToHtmlAsync(options, 2);
            var header2Value = await header2.ToHtmlAsync(options, 2);
            await htmlStreamWriterMock.Received(1).WriteLineAsync(HtmlIndicators.DocType);
            await htmlStreamWriterMock.Received(1).WriteLineAsync(HtmlIndicators.Html.ToHtmlStartTag());
            await htmlStreamWriterMock.Received(1).WriteLineAsync(HtmlIndicators.Body.ToHtmlStartTag(), 1);
            await htmlStreamWriterMock.Received(1).WriteAsync(header1Value);
            await htmlStreamWriterMock.Received(1).WriteAsync(header2Value);
            await htmlStreamWriterMock.Received(1).WriteLineAsync(HtmlIndicators.Body.ToHtmlEndTag(), 1);
            await htmlStreamWriterMock.Received(1).WriteLineAsync(HtmlIndicators.Html.ToHtmlEndTag());
            await htmlStreamWriterMock.Received(1).FlushAsync();
        }
    }
}
