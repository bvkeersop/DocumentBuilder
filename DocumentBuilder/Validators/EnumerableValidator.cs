using DocumentBuilder.Enumerations;
using DocumentBuilder.Exceptions;

namespace DocumentBuilder.Validators
{
    public interface IEnumerableValidator
    {
        EmptyEnumerableBehavior Behavior { get; set; }
        bool ShouldRender<TValue>(IEnumerable<TValue> enumerable);
    }

    public class EnumerableValidator : IEnumerableValidator
    {
        public EmptyEnumerableBehavior Behavior { get; set; }

        public EnumerableValidator(EmptyEnumerableBehavior behavior)
        {
            Behavior = behavior;
        }

        public bool ShouldRender<TValue>(IEnumerable<TValue> enumerable)
        {
            if (!enumerable.Any() && Behavior == EmptyEnumerableBehavior.ThrowException)
            {
                throw new DocumentBuilderException(DocumentBuilderErrorCode.ProvidedEnumerableIsEmpty);
            }

            if (!enumerable.Any() && Behavior == EmptyEnumerableBehavior.SkipRender)
            {
                return false;
            }

            return true;
        }
    }
}
