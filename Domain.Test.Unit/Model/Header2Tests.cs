using NDocument.Domain.Constants;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Model;

namespace NDocument.Domain.Test.Unit.Model
{
    [TestClass]
    public class Header2Tests : HeaderTestBase
    {
        private Header2 _header2;

        [TestInitialize]
        public void TestInitialize()
        {
            _header2 = new Header2(_value);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToMarkdown_ReturnsMarkdownHeader1(LineEndings lineEndings)
        {
            // Act & Assert
            await AssertToMarkdownReturnsCorrectMarkdownHeader(_header2, MarkdownIndicators.Header2, lineEndings);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment, IndentationType.Spaces, 2, 0)]
        [DataRow(LineEndings.Environment, IndentationType.Spaces, 2, 1)]
        [DataRow(LineEndings.Environment, IndentationType.Spaces, 4, 2)]
        [DataRow(LineEndings.Windows, IndentationType.Spaces, 2, 0)]
        [DataRow(LineEndings.Linux, IndentationType.Spaces, 2, 0)]
        public async Task ToHtml_ReturnsHtmlHeader1(
            LineEndings lineEndings,
            IndentationType indentationType,
            int indentationSize,
            int indentationLevel)
        {
            // Act & Assert
            await AssertToHtmlReturnsCorrectHtmlHeader(_header2, HtmlIndicators.Header2, lineEndings, indentationType, indentationSize, indentationLevel);
        }
    }
}