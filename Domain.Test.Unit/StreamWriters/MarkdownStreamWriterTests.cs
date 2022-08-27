using DocumentBuilder.StreamWriters;
using DocumentBuilder.Test.Unit.Mocks;
using DocumentBuilder.Utilities;
using FluentAssertions;
using NSubstitute;

namespace DocumentBuilder.Test.Unit.StreamWriters
{
    [TestClass]
    public class MarkdownStreamWriterTests
    {
        private INewLineProvider _newLineProviderMock;
        private StreamWriterMock _streamWriterMock;
        private MarkdownStreamWriter _markdownStreamWriter;

        [TestInitialize]
        public void TestInitialize()
        {
            _newLineProviderMock = Substitute.For<INewLineProvider>();
            _streamWriterMock = new StreamWriterMock();
            _markdownStreamWriter = new MarkdownStreamWriter(_streamWriterMock, _newLineProviderMock);
        }

        [TestMethod]
        public async Task WriteAsync_StringValue_StreamWriterReceivesWrite()
        {
            // Arrange
            var stringValue = "stringValue";

            // Act
            await _markdownStreamWriter.WriteAsync(stringValue);

            // Assert
            _streamWriterMock.HasReceivedWrite(stringValue);
            _newLineProviderMock.Received(0).GetNewLine();
        }

        [TestMethod]
        public async Task WriteAsync_CharValue_StreamWriterReceivesWrite()
        {
            // Arrange
            var charValue = "charValue";

            // Act
            await _markdownStreamWriter.WriteAsync(charValue);

            // Assert
            _streamWriterMock.HasReceivedWrite(charValue);
            _newLineProviderMock.Received(0).GetNewLine();
        }

        [TestMethod]
        public async Task WriteLineAsync_StringValue_StreamWriterReceivesWriteLine()
        {
            // Arrange
            var stringValue = "stringValue";

            // Act
            await _markdownStreamWriter.WriteLineAsync(stringValue);

            // Assert
            _streamWriterMock.HasReceivedWriteLine(stringValue);
            _newLineProviderMock.Received(1).GetNewLine();
        }

        [TestMethod]
        public async Task WriteNewLineAsync_StreamWriterReceivesWriteLine()
        {
            // Act
            await _markdownStreamWriter.WriteNewLineAsync();

            // Assert
            _streamWriterMock.WriteStringValuesReceived.Count().Should().Be(1);
            _newLineProviderMock.Received(1).GetNewLine();
        }

        [TestMethod]
        public async Task FlushAsync_StreamWriterReceivesFlush()
        {
            // Act
            await _markdownStreamWriter.FlushAsync();

            // Assert
            _streamWriterMock.AmountOfFlushes.Should().Be(1);
            _newLineProviderMock.Received(0).GetNewLine();
        }
    }
}
