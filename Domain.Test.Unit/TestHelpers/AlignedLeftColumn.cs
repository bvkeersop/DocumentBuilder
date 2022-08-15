using DocumentBuilder.Domain.Enumerations;
using DocumentBuilder.Domain.Attributes;

namespace DocumentBuilder.Domain.Test.Unit.TestHelpers
{
    internal class AlignedLeftColumn : AlignedColumn
    {
        [Column(alignment: Alignment.Left)]
        public string ColumnName { get; set; } = "ColumnValue";
    }
}
