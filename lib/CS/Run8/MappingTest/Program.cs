namespace MappingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: CompareExeVersions <path_to_old_exe> <path_to_new_exe>");
                return;
            }

            string oldExePath = args[0];
            string newExePath = args[1];

            var oldClasses = Extractor.GetClassesFromAssembly(oldExePath);
            var newClasses = Extractor.GetClassesFromAssembly(newExePath);

            var pairs = PairGenerator.GeneratePairs(oldClasses, newClasses);

            Labeling.AutoLabeling(pairs);

            foreach (var pair in pairs)
            {
                Console.WriteLine($"Old Class: {pair.OldClass.ClassName}, New Class: {pair.NewClass.ClassName}, IsSameClass: {pair.IsSameClass}");
            }
        }
    }
}