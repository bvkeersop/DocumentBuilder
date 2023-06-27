using DocumentBuilder.Core.Enumerations;
using DocumentBuilder.Utilities;

namespace DocumentBuilder.Factories;

public static class IndentationProviderFactory
{
    public static IIndentationProvider Create(IndentationType indentationType, int indentationSize, int rootIndentationLevel = 0) => indentationType switch
    {
        IndentationType.Spaces => new SpaceIdentationProvider(indentationSize, rootIndentationLevel),
        IndentationType.Tabs => new TabIdentationProvider(indentationSize, rootIndentationLevel),
        _ => throw new NotSupportedException($"{indentationType} is currently not supported")
    };
}
