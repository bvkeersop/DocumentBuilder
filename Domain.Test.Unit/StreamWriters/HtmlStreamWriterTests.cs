using FluentAssertions;
using NDocument.Domain.StreamWriters;
using NDocument.Domain.Test.Unit.Mocks;
using NDocument.Domain.Utilities;
using NSubstitute;

namespace NDocument.Domain.Test.Unit.StreamWriters
{
    internal class HtmlStreamWriterTests
    {
        private INewLineProvider _newLineProviderMock;
        private IIndentationProvider _indentationProviderMock;
        private StreamWriterMock _streamWriterMock;
        private HtmlStreamWriter _htmlStreamWriter;

        [TestInitialize]
        public void TestInitialize()
        {
            _newLineProviderMock = Substitute.For<INewLineProvider>();
            _indentationProviderMock = Substitute.For<IIndentationProvider>();
            _streamWriterMock = new StreamWriterMock();
            _htmlStreamWriter = new HtmlStreamWriter(_streamWriterMock, _newLineProviderMock, _indentationProviderMock);
        }

        [TestMethod]
        public async Task WriteAsync_StringValue_StreamWriterReceivesWrite()
        {
            // Arrange
            var stringValue = "stringValue";
            var indentationLevel = 1;

            // Act
            await _htmlStreamWriter.WriteAsync(stringValue, indentationLevel);

            // Assert
            _streamWriterMock.HasReceivedWrite(stringValue);
            _newLineProviderMock.Received(0).GetNewLine();
            _indentationProviderMock.Received(1).GetIndentation(indentationLevel);
        }

        [TestMethod]
        public async Task WriteLineAsync_StringValue_StreamWriterReceivesWriteLine()
        {
            // Arrange
            var stringValue = "stringValue";
            var indentationLevel = 1;

            // Act
            await _htmlStreamWriter.WriteAsync(stringValue, indentationLevel);

            // Assert
            _streamWriterMock.HasReceivedWriteLine(stringValue);
            _newLineProviderMock.Received(1).GetNewLine();
            _indentationProviderMock.Received(1).GetIndentation(indentationLevel);
        }

        [TestMethod]
        public async Task WriteNewLineAsync_StreamWriterReceivesWriteLine()
        {
            // Act
            await _htmlStreamWriter.WriteNewLineAsync();

            // Assert
            _streamWriterMock.HasReceivedWriteLine(_newLineProviderMock.GetNewLine());
            _newLineProviderMock.Received(1).GetNewLine();
            _indentationProviderMock.Received(0).GetIndentation(Arg.Any<int>());
        }

        [TestMethod]
        public async Task FlushAsync_StreamWriterReceivesFlush()
        {
            // Act
            await _htmlStreamWriter.FlushAsync();

            // Assert
            _streamWriterMock.AmountOfFlushes.Should().Be(1);
            _newLineProviderMock.Received(0).GetNewLine();
            _indentationProviderMock.Received(0).GetIndentation(Arg.Any<int>());
        }
    }
}
