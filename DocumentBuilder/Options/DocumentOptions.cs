using DocumentBuilder.Enumerations;

namespace DocumentBuilder.Options
{
    public class DocumentOptions
    {
        /// <summary>
        /// The line ending to use
        /// </summary>
        public LineEndings LineEndings { get; set; }

        /// <summary>
        /// What behavior to display when a builder gets passed an empty enumerable
        /// </summary>
        public EmptyEnumerableBehavior BehaviorOnEmptyEnumerable { get; set; }
    }
}
