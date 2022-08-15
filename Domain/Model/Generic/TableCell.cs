namespace DocumentBuilder.Domain.Model.Generic
{
    public class TableCell
    {
        public string Value { get; }
        public Type Type { get; }
        public int RowPosition { get; }
        public int ColumnPosition { get; }

        public TableCell(string value, Type type, int rowPosition, int columnPosition)
        {
            Value = value;
            Type = type;
            RowPosition = rowPosition;
            ColumnPosition = columnPosition;
        }
    }
}
