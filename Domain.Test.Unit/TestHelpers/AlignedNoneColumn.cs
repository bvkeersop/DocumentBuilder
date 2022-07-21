using NDocument.Domain.Attributes;
using NDocument.Domain.Enumerations;

namespace NDocument.Domain.Test.Unit.TestHelpers
{
    internal class AlignedNoneColumn : AlignedColumn
    {
        [Column(alignment: Alignment.None)]
        public string ColumnName { get; set; } = "ColumnValue";
    }
}
