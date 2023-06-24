using DocumentBuilder.Enumerations;
using DocumentBuilder.Exceptions;
using DocumentBuilder.Validators;
using FluentAssertions;

namespace DocumentBuilder.Test.Unit.Validators
{
    [TestClass]
    public class EnumerableValidatorTests
    {
        [TestMethod]
        public void ShouldRender_EmptyEnumerableAndSkipRender_ReturnsFalse()
        {
            // Arrange
            var emptyEnumerable = Enumerable.Empty<string>();
            var enumerableValidator = new EnumerableValidator(EmptyRenderingStrategy.SkipRender);

            // Act
            var shouldRender = enumerableValidator.ShouldRender(emptyEnumerable);

            // Assert
            shouldRender.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldRender_EmptyEnumerableAndRender_ReturnsTrue()
        {
            // Arrange
            var emptyEnumerable = Enumerable.Empty<string>();
            var enumerableValidator = new EnumerableValidator(EmptyRenderingStrategy.Render);

            // Act
            var shouldRender = enumerableValidator.ShouldRender(emptyEnumerable);

            // Assert
            shouldRender.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldRender_EmptyEnumerableAndThrowException_ThrowsException()
        {
            // Arrange
            var emptyEnumerable = Enumerable.Empty<string>();
            var enumerableValidator = new EnumerableValidator(EmptyRenderingStrategy.ThrowException);

            // Act
            var action = () => enumerableValidator.ShouldRender(emptyEnumerable);

            // Assert
            var exception = action.Should().Throw<DocumentBuilderException>()
                .Where(e => e.ErrorCode == DocumentBuilderErrorCode.ProvidedEnumerableIsEmpty);
        }
    }
}
