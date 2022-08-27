using DocumentBuilder.Enumerations;
using DocumentBuilder.Utilities;

namespace DocumentBuilder.Factories
{
    internal static class IndentationProviderFactory
    {
        public static IIndentationProvider Create(IndentationType indentationType, int indentationSize, int rootIndentationLevel = 0)
        {
            return indentationType switch
            {
                IndentationType.Spaces => new SpaceIdentationProvider(indentationSize, rootIndentationLevel),
                IndentationType.Tabs => new TabIdentationProvider(indentationSize, rootIndentationLevel),
                _ => throw new NotSupportedException($"{indentationType} is currently not supported")
            };
        }
    }
}
