using NDocument.Domain.Exceptions;

namespace NDocument.Domain.Utilities
{
    public static class ExcelColumnIdentifierGenerator
    {
        public static string GenerateColumnIdentifier(int index)
        {
            if (index < 1)
            {
                throw new NDocumentException(NDocumentErrorCode.IdentifierMustBeGreaterThanZero);
            }

            var columnName = string.Empty;

            while (index > 0)
            {
                int modulo = (index - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                index = (index - modulo) / 26;
            }

            return columnName;
        }
    }
}