using DocumentBuilder.Domain.Interfaces;
using DocumentBuilder.Domain.Options;
using DocumentBuilder.Domain.DocumentWriters;
using DocumentBuilder.Domain.Model.Generic;
using DocumentBuilder.Domain.StreamWriters;
using NSubstitute;

namespace DocumentBuilder.Domain.Test.Unit.DocumentWriters
{
    [TestClass]
    public class MarkdownDocumentWriterTests
    {
        [TestMethod]
        public async Task WriteToStreamAsync_OneConvertable_CallsWriteAsync()
        {
            // Arrange
            var options = new MarkdownDocumentOptions();
            var memoryStream = new MemoryStream();
            var header1 = new Header1("header1");
            var markdownStreamWriterMock = Substitute.For<IMarkdownStreamWriter>();
            var markdownConvertibles = new List<IMarkdownConvertable>
            {
                header1
            };

            Func<Stream, MarkdownDocumentOptions, IMarkdownStreamWriter> factory
                = (outputStream, options) => markdownStreamWriterMock;

            var markdownDocumentWriter = new MarkdownDocumentWriter(factory, options);

            // Act
            await markdownDocumentWriter.WriteToStreamAsync(memoryStream, markdownConvertibles);

            // Assert
            var header1Value = await header1.ToMarkdownAsync(options);
            await markdownStreamWriterMock.Received(1).WriteAsync(header1Value);
            await markdownStreamWriterMock.Received(1).FlushAsync();
        }

        [TestMethod]
        public async Task WriteToStreamAsync_TwoConvertables_CallsWriteLineAndWriteAsync()
        {
            // Arrange
            var options = new MarkdownDocumentOptions();
            var memoryStream = new MemoryStream();
            var header1 = new Header1("header1");
            var header2 = new Header2("header2");
            var markdownStreamWriterMock = Substitute.For<IMarkdownStreamWriter>();
            var markdownConvertibles = new List<IMarkdownConvertable>
            {
                header1,
                header2
            };


            Func<Stream, MarkdownDocumentOptions, IMarkdownStreamWriter> factory
                = (outputStream, options) => markdownStreamWriterMock;

            var markdownDocumentWriter = new MarkdownDocumentWriter(factory, options);

            // Act
            await markdownDocumentWriter.WriteToStreamAsync(memoryStream, markdownConvertibles);

            // Assert
            var header1Value = await header1.ToMarkdownAsync(options);
            var header2Value = await header2.ToMarkdownAsync(options);
            await markdownStreamWriterMock.Received(1).WriteLineAsync(header1Value);
            await markdownStreamWriterMock.Received(1).WriteAsync(header2Value);
            await markdownStreamWriterMock.Received(1).FlushAsync();
        }
    }
}
