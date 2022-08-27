using DocumentBuilder.Constants;
using DocumentBuilder.Enumerations;
using DocumentBuilder.Model.Generic;

namespace DocumentBuilder.Test.Unit.Model.Generic
{
    [TestClass]
    public class Header4Tests : HeaderTestBase
    {
        private Header4 _header4;

        [TestInitialize]
        public void TestInitialize()
        {
            _header4 = new Header4(_value);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToMarkdown_ReturnsMarkdownHeader1(LineEndings lineEndings)
        {
            // Act & Assert
            await AssertToMarkdownReturnsCorrectMarkdownHeader(_header4, MarkdownIndicators.Header4, lineEndings);
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
            await AssertToHtmlReturnsCorrectHtmlHeader(_header4, HtmlIndicators.Header4, lineEndings, indentationType, indentationSize, indentationLevel);
        }
    }
}
