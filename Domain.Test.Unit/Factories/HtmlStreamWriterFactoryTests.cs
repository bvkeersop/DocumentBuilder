using DocumentBuilder.Domain.Factories;
using DocumentBuilder.Domain.Options;
using FluentAssertions;

namespace DocumentBuilder.Domain.Test.Unit.Factories
{
    [TestClass]
    public class HtmlStreamWriterFactoryTests
    {
        [TestMethod]
        public void Create_ReturnsHtmlStreamWriter()
        {
            // Arrange
            var options = new HtmlDocumentOptions();
            var outputStream = new MemoryStream();

            // Act
            var htmlStreamWriter = HtmlStreamWriterFactory.Create(outputStream, options);

            // Assert
            htmlStreamWriter.Should().NotBeNull();
        }
    }
}