using DocumentBuilder.Markdown.Model;
using DocumentBuilder.Markdown.Options;
using FluentAssertions;

namespace DocumentBuilder.Markdown.Test.Unit.Model;

[TestClass]
public class Header2Tests
{
    [TestMethod]
    public void ToMarkdown_ReturnsMarkdownHeader1()
    {
        // Arrange
        var value = "header2";
        var header1 = new Header2(value);
        var options = new MarkdownDocumentOptions();

        // Act
        var markdown = header1.ToMarkdown(options);
        markdown.Should().Be("## header2");
    }
}
