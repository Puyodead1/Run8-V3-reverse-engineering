namespace MappingTest
{
    public class ClassPair
    {
        public ClassInfo OldClass { get; set; }
        public ClassInfo NewClass { get; set; }
        public bool IsSameClass { get; set; } // Label: true if they are the same class, false otherwise
    }
}
