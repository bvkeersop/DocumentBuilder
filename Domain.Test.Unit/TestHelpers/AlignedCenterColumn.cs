using NDocument.Domain.Attributes;
using NDocument.Domain.Enumerations;

namespace NDocument.Domain.Test.Unit.TestHelpers
{
    internal class AlignedCenterColumn : AlignedColumn
    {
        [Column(alignment: Alignment.Center)]
        public string ColumnName { get; set; } = "ColumnValue";
    }
}
