namespace DocumentBuilder.Domain.Exceptions
{
    public enum DocumentBuilderErrorCode
    {
        Unknown,
        ProvidedTableIsEmpty,
        StreamIsNotWriteable,
        CouldNotFindColumnAtIndex,
        CouldNotFindTableRowAtIndex,
        ColumnHasNoName,
        IdentifierMustBeGreaterThanZero,
        NoWorksheetInstantiated
    }
}
