using DocumentBuilder.Markdown.Model;
using DocumentBuilder.Markdown.Options;
using FluentAssertions;

namespace DocumentBuilder.Markdown.Test.Unit.Model;

[TestClass]
public class Header4Tests
{
    [TestMethod]
    public void ToMarkdown_ReturnsMarkdownHeader1()
    {
        // Arrange
        var value = "header4";
        var header1 = new Header4(value);
        var options = new MarkdownDocumentOptions();

        // Act
        var markdown = header1.ToMarkdown(options);
        markdown.Should().Be("#### header4");
    }
}
