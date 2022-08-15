using DocumentBuilder.Domain.Enumerations;
using DocumentBuilder.Domain.Attributes;

namespace DocumentBuilder.Domain.Test.Unit.TestHelpers
{
    internal class AlignedNoneColumn : AlignedColumn
    {
        [Column(alignment: Alignment.None)]
        public string ColumnName { get; set; } = "ColumnValue";
    }
}
