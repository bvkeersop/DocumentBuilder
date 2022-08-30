using DocumentBuilder.Enumerations;
using DocumentBuilder.Exceptions;

namespace DocumentBuilder.Validators
{
    public interface IEnumerableValidator
    {
        bool ShouldRender<TValue>(IEnumerable<TValue> enumerable);
    }

    public class EnumerableValidator : IEnumerableValidator
    {
        private readonly EmptyEnumerableBehavior _behavior;

        public EnumerableValidator(EmptyEnumerableBehavior behavior)
        {
            _behavior = behavior;
        }


        public bool ShouldRender<TValue>(IEnumerable<TValue> enumerable)
        {
            if (!enumerable.Any() && _behavior == EmptyEnumerableBehavior.ThrowException)
            {
                throw new DocumentBuilderException(DocumentBuilderErrorCode.ProvidedEnumerableIsEmpty);
            }

            if (!enumerable.Any() && _behavior == EmptyEnumerableBehavior.Render)
            {
                return false;
            }

            return true;
        }
    }
}
