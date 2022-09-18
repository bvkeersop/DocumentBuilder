using DocumentBuilder.Constants;
using DocumentBuilder.Enumerations;
using DocumentBuilder.Factories;
using DocumentBuilder.Model.Markdown;
using DocumentBuilder.Options;
using FluentAssertions;

namespace DocumentBuilder.Test.Unit.Model.Markdown
{
    [TestClass]
    public class HorizontalRuleTests
    {
        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToMarkdownAsync_CreatesBlockquote(LineEndings lineEndings)
        {
            // Arrange
            var horizontalRule = new HorizontalRule();

            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            var options = new MarkdownDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act
            var markdown = await horizontalRule.ToMarkdownAsync(options);

            // Assert
            var expectedMarkdown = $"{MarkdownIndicators.HorizontalRule}{newLineProvider.GetNewLine()}";
            markdown.Should().Be(expectedMarkdown);
        }
    }
}
