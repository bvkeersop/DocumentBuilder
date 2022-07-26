﻿using DocumentBuilder.Enumerations;
using DocumentBuilder.Attributes;

namespace DocumentBuilder.Test.Unit.TestHelpers
{
    internal class AlignedRightColumn
    {
        [Column(alignment: Alignment.Right)]
        public string ColumnName { get; set; } = "ColumnValue";
    }
}
