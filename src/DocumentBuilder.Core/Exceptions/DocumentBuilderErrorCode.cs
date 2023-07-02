namespace DocumentBuilder.Exceptions
{
    public enum DocumentBuilderErrorCode
    {
        Unknown,
        CouldNotFindColumnAtIndex,
        CouldNotFindTableRowAtIndex,
        ProvidedEnumerableIsEmpty,
        ProvidedGenericTypeForTableDoesNotEqualRunTimeType,
    }
}
