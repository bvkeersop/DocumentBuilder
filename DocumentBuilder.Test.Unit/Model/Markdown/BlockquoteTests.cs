using DocumentBuilder.Constants;
using DocumentBuilder.Enumerations;
using DocumentBuilder.Factories;
using DocumentBuilder.Model.Markdown;
using DocumentBuilder.Options;
using FluentAssertions;

namespace DocumentBuilder.Test.Unit.Model.Markdown
{
    [TestClass]
    public class BlockquoteTests
    {
        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToMarkdownAsync_CreatesBlockquote(LineEndings lineEndings)
        {
            // Arrange
            var value = "blockquote";
            var blockquote = new Blockquote(value);

            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            var options = new MarkdownDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act
            var markdown = await blockquote.ToMarkdownAsync(options);

            // Assert
            var expectedMarkdown = $"{MarkdownIndicators.Blockquote} {value}{newLineProvider.GetNewLine()}";
            markdown.Should().Be(expectedMarkdown);
        }
    }
}
