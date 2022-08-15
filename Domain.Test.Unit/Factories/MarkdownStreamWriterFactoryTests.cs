using DocumentBuilder.Domain.Factories;
using DocumentBuilder.Domain.Options;
using FluentAssertions;

namespace DocumentBuilder.Domain.Test.Unit.Factories
{
    [TestClass]
    public class MarkdownStreamWriterFactoryTests
    {
        [TestMethod]
        public void Create_ReturnsMarkdownStreamWriter()
        {
            // Arrange
            var options = new MarkdownDocumentOptions();
            var outputStream = new MemoryStream();

            // Act
            var htmlStreamWriter = MarkdownStreamWriterFactory.Create(outputStream, options);

            // Assert
            htmlStreamWriter.Should().NotBeNull();
        }
    }
}