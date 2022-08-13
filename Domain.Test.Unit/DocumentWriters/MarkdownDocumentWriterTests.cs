using NDocument.Domain.DocumentWriters;
using NDocument.Domain.Interfaces;
using NDocument.Domain.Model.Generic;
using NDocument.Domain.Options;
using NDocument.Domain.StreamWriters;
using NSubstitute;

namespace NDocument.Domain.Test.Unit.DocumentWriters
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
            var markdownStreamWriter = Substitute.For<IMarkdownStreamWriter>();
            var markdownConvertibles = new List<IMarkdownConvertable>
            {
                header1
            };


            Func<Stream, MarkdownDocumentOptions, IMarkdownStreamWriter> factory
                = (Stream outputStream, MarkdownDocumentOptions options) => markdownStreamWriter;

            var markdownDocumentWriter = new MarkdownDocumentWriter(factory, options);

            // Act
            await markdownDocumentWriter.WriteToStreamAsync(memoryStream, markdownConvertibles);

            // Assert
            var header1Value = await header1.ToMarkdownAsync(options);
            await markdownStreamWriter.Received(1).WriteAsync(header1Value);
            await markdownStreamWriter.Received(1).FlushAsync();
        }

        [TestMethod]
        public async Task WriteToStreamAsync_TwoConvertables_CallsWriteLineAndWriteAsync()
        {
            // Arrange
            var options = new MarkdownDocumentOptions();
            var memoryStream = new MemoryStream();
            var header1 = new Header1("header1");
            var header2 = new Header2("header2");
            var markdownStreamWriter = Substitute.For<IMarkdownStreamWriter>();
            var markdownConvertibles = new List<IMarkdownConvertable>
            {
                header1,
                header2
            };


            Func<Stream, MarkdownDocumentOptions, IMarkdownStreamWriter> factory
                = (Stream outputStream, MarkdownDocumentOptions options) => markdownStreamWriter;

            var markdownDocumentWriter = new MarkdownDocumentWriter(factory, options);

            // Act
            await markdownDocumentWriter.WriteToStreamAsync(memoryStream, markdownConvertibles);

            // Assert
            var header1Value = await header1.ToMarkdownAsync(options);
            var header2Value = await header2.ToMarkdownAsync(options);
            await markdownStreamWriter.Received(1).WriteLineAsync(header1Value);
            await markdownStreamWriter.Received(1).WriteAsync(header2Value);
            await markdownStreamWriter.Received(1).FlushAsync();
        }
    }
}
