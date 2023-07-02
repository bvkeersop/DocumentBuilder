using DocumentBuilder.Exceptions;
using DocumentBuilder.Model.Html;
using FluentAssertions;

namespace DocumentBuilder.Test.Unit.Model.Html
{
    [TestClass]
    public class HtmlAttributesTests
    {
        private HtmlAttributes _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new HtmlAttributes();
        }

        [TestMethod]
        public void AddAttribute_NewAttribute_AttributeAddedSuccessfully()
        {
            // Arrange
            var key = "class";
            var value = "a-test-value";

            // Act
            _sut.Add(key, value);

            // Assert
            _sut.Count.Should().Be(1);
            _sut.ToString().Should().Be($"{key}=\"{value}\"");
        }

        [TestMethod]
        public void AddAttribute_DuplicateValueAdded_IgnoresSecondAdd()
        {
            // Arrange
            var key = "class";
            var value = "a-test-value";
            _sut.Add(key, value);

            // Act
            _sut.Add(key, value);

            // Assert
            _sut.Count.Should().Be(1);
            _sut.ToString().Should().Be($"{key}=\"{value}\"");
        }

        [TestMethod]
        public void AddAttribute_ExistingNotUniqueAttribute_AttributeAddedSuccessfully()
        {
            // Arrange
            var key = "class";
            var value = "a-test-value";
            var secondValue = "a-second-test-value";
            _sut.Add(key, value);

            // Act
            _sut.Add(key, secondValue);

            // Assert
            _sut.Count.Should().Be(1);
            _sut.ToString().Should().Be($"{key}=\"{value} {secondValue}\"");
        }

        [TestMethod]
        public void AddAttribute_ExistingUniqueAttribute_ThrowsException()
        {
            // Arrange
            var key = "id";
            var value = "a-test-value";
            var secondValue = "a-second-test-value";
            _sut.Add(key, value);

            // Act
            var action = () => _sut.Add(key, secondValue);

            // Assert
            var e = action.Should().Throw<DocumentBuilderException>()
                .Where(e => e.ErrorCode == DocumentBuilderErrorCode.AttemptedToAddDuplicateUniqueHtmlAttribute);
        }
    }
}
