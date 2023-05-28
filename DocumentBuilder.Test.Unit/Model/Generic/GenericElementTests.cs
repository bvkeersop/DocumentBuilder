using DocumentBuilder.Constants;
using DocumentBuilder.DocumentBuilders;
using DocumentBuilder.Model.Generic;
using FluentAssertions;

namespace DocumentBuilder.Test.Unit.Model.Generic
{
    [TestClass]
    public class GenericElementTests
    {
        [TestMethod]
        public void GetHtmlStartTagWithAttributes_ReturnsHtmlStartTagWithAttributes()
        {
            // Arrange
            var htmlDocumentBuilder = (HtmlDocumentBuilder)new HtmlDocumentBuilder().AddHeader1("test").WithId("some-id");
            var header1 = (Header1)htmlDocumentBuilder.HtmlConvertables.First();

            // Act
            header1.GetHtmlStartTagWithAttributes(HtmlIndicators.Header1).Should().Be("<h1 id=\"some-id\">");
        }
    }
}
