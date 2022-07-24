namespace NDocument.Domain.Exceptions
{
    public enum NDocumentErrorCode
    {
        Unknown,
        ProvidedTableIsEmpty,
        TableCellValueIsntOfTypeString,
        StreamIsNotWriteable,
        CouldNotFindColumnAtIndex,
        CouldNotFindTableRowAtIndex,
        ColumnHasNoName,
        IdentifierMustBeGreaterThanZero,
    }
}
