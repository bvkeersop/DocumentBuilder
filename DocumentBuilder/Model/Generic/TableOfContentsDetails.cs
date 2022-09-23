namespace DocumentBuilder.Model.Generic
{
    internal class TableOfContentsDetails
    {
        public bool ShouldAdd { get; set; }
        public int AtIndex { get; set; } = 0;
        public bool IsNumbered { get; set; }
    }
}
