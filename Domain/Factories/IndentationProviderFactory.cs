using NDocument.Domain.Enumerations;
using NDocument.Domain.Utilities;

namespace NDocument.Domain.Factories
{
    internal static class IndentationProviderFactory
    {
        public static IIndentationProvider Create(IndentationType indentationType, int indentationSize)
        {
            return indentationType switch
            {
                IndentationType.Spaces => new SpaceIdentationProvider(indentationSize),
                IndentationType.Tabs => new TabIdentationProvider(indentationSize),
                _ => throw new NotSupportedException($"{indentationType} is currently not supported")
            };
        }
    }
}
