using DocumentBuilder.Constants;
using DocumentBuilder.Enumerations;
using DocumentBuilder.Factories;
using DocumentBuilder.Model.Markdown;
using DocumentBuilder.Options;
using FluentAssertions;

namespace DocumentBuilder.Test.Unit.Model.Markdown
{
    [TestClass]
    public class FencedCodeblockTests
    {
        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToMarkdownAsync_LanguageProvided_CreatesCodeblockWithLanguage(LineEndings lineEndings)
        {
            // Arrange
            var value = "fencedCodeblock";
            var language = "C#";
            var codeblock = new FencedCodeblock(value, language);

            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            var options = new MarkdownDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act
            var markdown = await codeblock.ToMarkdownAsync(options);

            // Assert
            var expectedMarkdown =
                $"{MarkdownIndicators.Codeblock}{language}{newLineProvider.GetNewLine()}" +
                value + newLineProvider.GetNewLine() +
                MarkdownIndicators.Codeblock + newLineProvider.GetNewLine();
            markdown.Should().Be(expectedMarkdown);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToMarkdownAsync_NoLanguageProvided_CreatesCodeblockWithoutLanguage(LineEndings lineEndings)
        {
            // Arrange
            var value = "fencedCodeblock";
            var codeblock = new FencedCodeblock(value);

            var newLineProvider = NewLineProviderFactory.Create(lineEndings);

            var options = new MarkdownDocumentOptions
            {
                LineEndings = lineEndings
            };

            // Act
            var markdown = await codeblock.ToMarkdownAsync(options);

            // Assert
            var expectedMarkdown =
                $"{MarkdownIndicators.Codeblock}{newLineProvider.GetNewLine()}" +
                    value + newLineProvider.GetNewLine() +
                  MarkdownIndicators.Codeblock + newLineProvider.GetNewLine();
            markdown.Should().Be(expectedMarkdown);
        }
    }
}
