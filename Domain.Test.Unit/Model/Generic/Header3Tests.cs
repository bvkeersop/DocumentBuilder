using NDocument.Domain.Constants;
using NDocument.Domain.Enumerations;
using NDocument.Domain.Model.Generic;

namespace NDocument.Domain.Test.Unit.Model.Generic
{
    [TestClass]
    public class Header3Tests : HeaderTestBase
    {
        private Header3 _header3;

        [TestInitialize]
        public void TestInitialize()
        {
            _header3 = new Header3(_value);
        }

        [DataTestMethod]
        [DataRow(LineEndings.Environment)]
        [DataRow(LineEndings.Windows)]
        [DataRow(LineEndings.Linux)]
        public async Task ToMarkdown_ReturnsMarkdownHeader1(LineEndings lineEndings)
        {
            // Act & Assert
            await AssertToMarkdownReturnsCorrectMarkdownHeader(_header3, MarkdownIndicators.Header3, lineEndings);
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
            await AssertToHtmlReturnsCorrectHtmlHeader(_header3, HtmlIndicators.Header3, lineEndings, indentationType, indentationSize, indentationLevel);
        }
    }
}