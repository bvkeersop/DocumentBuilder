using DocumentBuilder.Utilities;
using DocumentBuilder.Enumerations;
using DocumentBuilder.Extensions;
using DocumentBuilder.Factories;
using DocumentBuilder.Model;
using DocumentBuilder.Options;

namespace DocumentBuilder.Test.Unit.Model
{
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
}
