using DocumentBuilder.Domain.Enumerations;
using DocumentBuilder.Domain.Attributes;

namespace DocumentBuilder.Domain.Test.Unit.TestHelpers
{
    internal class AlignedRightColumn : AlignedColumn
    {
        [Column(alignment: Alignment.Right)]
        public string ColumnName { get; set; } = "ColumnValue";
    }
}
