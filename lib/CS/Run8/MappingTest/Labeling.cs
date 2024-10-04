namespace MappingTest
{
    public class Labeling
    {
        public static void ManualLabeling(List<ClassPair> pairs)
        {
            // Implement a manual labeling interface or tool
            // For simplicity, this is a placeholder for manual labeling
            foreach (var pair in pairs)
            {
                Console.WriteLine($"Old Class: {pair.OldClass.ClassName}, New Class: {pair.NewClass.ClassName}");
                Console.WriteLine("Are these the same class? (y/n)");
                var input = Console.ReadLine();
                pair.IsSameClass = input.ToLower() == "y";
            }
        }

        public static void AutoLabeling(List<ClassPair> pairs)
        {
            // Implement heuristic rules for automatic labeling
            foreach (var pair in pairs)
            {
                pair.IsSameClass = pair.OldClass.ClassName == pair.NewClass.ClassName;
            }
        }
    }
}
