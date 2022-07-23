using System.Text;

namespace NDocument.Domain.Utilities
{
    public interface IIndentationProvider
    {
        string GetIndentation(int level);
    }

    public class SpaceIdentationProvider : IdentationProviderBase, IIndentationProvider
    {
        public SpaceIdentationProvider(int identationSize, int rootIndentationLevel) : base(identationSize, " ", rootIndentationLevel)
        {
        }

        public string GetIndentation(int indentationLevel)
        {
            return CreateIndentation(indentationLevel);
        }
    }

    public class TabIdentationProvider : IdentationProviderBase, IIndentationProvider
    {
        public TabIdentationProvider(int identationSize, int rootIndentationLevel) : base(identationSize, "    ", rootIndentationLevel)
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
        private readonly int _rootIndentationLevel;
        private readonly int _indentationSize;

        public IdentationProviderBase(int identationSize, string indentationCharacter, int rootIndentationLevel)
        {
            _indentationCharacter = indentationCharacter;
            _rootIndentationLevel = rootIndentationLevel;
            _indentationSize = identationSize;
        }

        protected string CreateIndentation(int indentationLevel)
        {
            var identation = (_rootIndentationLevel * _indentationSize) + (_indentationSize * indentationLevel);

            return new StringBuilder(_indentationCharacter.Length * identation)
                .Insert(0, _indentationCharacter, identation)
                .ToString();
        }
    }
}
