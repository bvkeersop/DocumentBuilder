using DocumentBuilder.Domain.Constants;
using DocumentBuilder.Domain.Extensions;
using DocumentBuilder.Domain.Factories;
using DocumentBuilder.Domain.Model.Generic;
using DocumentBuilder.Domain.Options;
using FluentAssertions;

namespace DocumentBuilder.Domain.Test.Unit.Model.Generic
{
    [TestClass]
    public class ImageTests
    {
        private string _name;
        private string _path;

        [TestInitialize]
        public void TestInitialize()
        {
            _name = "image";
            _path = "./image";
        }

        [TestMethod]
        public async Task ToMarkdown_WithCaption_ReturnsMarkdownImageWithCaption()
        {
            // Arrange
            var options = new MarkdownDocumentOptions();
            var caption = "this is an image";
            var image = new Image(_name, _path, caption);

            // Act
            var markdownImage = await image.ToMarkdownAsync(options);

            // Assert
            var newLine = NewLineProviderFactory.Create(options.LineEndings).GetNewLine();
            markdownImage.Should().Be($"![{_name}]({_path}){newLine}*{caption}*{newLine}");
        }

        [TestMethod]
        public async Task ToMarkdown_WithoutCaption_ReturnsMarkdownImageWithoutCaption()
        {
            // Arrange
            var options = new MarkdownDocumentOptions();
            var image = new Image(_name, _path);

            // Act
            var markdownImage = await image.ToMarkdownAsync(options);

            // Assert
            var newLine = NewLineProviderFactory.Create(options.LineEndings).GetNewLine();
            markdownImage.Should().Be($"![{_name}]({_path}){newLine}");
        }

        [TestMethod]
        public async Task ToHtml_WithCaption_ReturnsHtmlImageWithCaption()
        {
            // Arrange
            var options = new HtmlDocumentOptions();
            var caption = "this is an image";
            var image = new Image(_name, _path, caption);

            // Act
            var htmlImageWithCaption = await image.ToHtmlAsync(options);

            // Assert
            var newLine = NewLineProviderFactory.Create(options.LineEndings).GetNewLine();
            var indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize);
            var indentation1 = indentationProvider.GetIndentation(1);
            var imageHtml = $"<{HtmlIndicators.Image} src=\"{_path}\" alt=\"{_name}\" />";
            var figureStartTag = HtmlIndicators.Figure.ToHtmlStartTag();
            var figureEndTag = HtmlIndicators.Figure.ToHtmlEndTag();
            var figCaptionStartTag = HtmlIndicators.FigCaption.ToHtmlStartTag();
            var figCaptionEndTag = HtmlIndicators.FigCaption.ToHtmlEndTag();

            var valueWithCaption = 
                $"{figureStartTag}{newLine}" +
                $"{indentation1}{imageHtml}{newLine}" +
                $"{indentation1}{figCaptionStartTag}{caption}{figCaptionEndTag}{newLine}" +
                $"{figureEndTag}{newLine}";

            htmlImageWithCaption.Should().Be(valueWithCaption);
        }

        [TestMethod]
        public async Task ToHtml_WithoutCaption_ReturnsHtmlImageWithoutCaption()
        {
            // Arrange
            var options = new HtmlDocumentOptions();
            var image = new Image(_name, _path);

            // Act
            var htmlImageWithoutCaption = await image.ToHtmlAsync(options);

            // Assert
            var newLine = NewLineProviderFactory.Create(options.LineEndings).GetNewLine();
            var indentationProvider = IndentationProviderFactory.Create(options.IndentationType, options.IndentationSize);
            var indentation1 = indentationProvider.GetIndentation(1);
            var imageHtml = $"<{HtmlIndicators.Image} src=\"{_path}\" alt=\"{_name}\" />";
            var figureStartTag = HtmlIndicators.Figure.ToHtmlStartTag();
            var figureEndTag = HtmlIndicators.Figure.ToHtmlEndTag();
            var valueWithoutCaption =
                $"{figureStartTag}{newLine}" +
                $"{indentation1}{imageHtml}{newLine}" +
                $"{figureEndTag}{newLine}";

            htmlImageWithoutCaption.Should().Be(valueWithoutCaption);
        }
    }
}
