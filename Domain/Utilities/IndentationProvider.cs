using System.Text;

namespace NDocument.Domain.Utilities
{
    public interface IIndentationProvider
    {
        string GetIndentation(int level);
    }

    public class SpaceIdentationProvider : IdentationProviderBase, IIndentationProvider
    {
        public SpaceIdentationProvider(int identationSize) : base(identationSize, " ")
        {
        }

        public string GetIndentation(int indentationLevel)
        {
            return CreateIndentation(indentationLevel);
        }
    }

    public class TabIdentationProvider : IdentationProviderBase, IIndentationProvider
    {
        public TabIdentationProvider(int identationSize) : base(identationSize, "    ")
        {
        }

        public string GetIndentation(int indentationLevel)
        {
            return CreateIndentation(indentationLevel);
        }
    }

    public abstract class IdentationProviderBase
    {
        private readonly string _indentationCharacter;
        private readonly int _indentationSize;

        public IdentationProviderBase(int identationSize, string indentationCharacter)
        {
            _indentationCharacter = indentationCharacter;
            _indentationSize = identationSize;
        }

        protected string CreateIndentation(int indentationLevel)
        {
            var identation = _indentationSize * indentationLevel;

            return new StringBuilder(_indentationCharacter.Length * identation)
                .Insert(0, _indentationCharacter, identation)
                .ToString();
        }
    }
}
