using DocumentBuilder.Utilities;

namespace DocumentBuilder.Test.Unit.Base;

public abstract class TestBase
{
    protected static string GetNewLineAndIndentation(INewLineProvider newLineProvider, IIndentationProvider indentationProvider, int level = 0)
    {
        return GetNewLine(newLineProvider) + GetIndentation(indentationProvider, level);
    }

    protected static string GetIndentation(IIndentationProvider indentationProvider, int level = 0)
    {
        return indentationProvider.GetIndentation(level);
    }

    protected static string GetNewLine(INewLineProvider newLineProvider)
    {
        return newLineProvider.GetNewLine();
    }
}
