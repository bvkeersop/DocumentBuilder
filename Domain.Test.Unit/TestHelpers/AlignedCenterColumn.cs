using DocumentBuilder.Domain.Enumerations;
using DocumentBuilder.Domain.Attributes;

namespace DocumentBuilder.Domain.Test.Unit.TestHelpers
{
    internal class AlignedCenterColumn : AlignedColumn
    {
        [Column(alignment: Alignment.Center)]
        public string ColumnName { get; set; } = "ColumnValue";
    }
}
