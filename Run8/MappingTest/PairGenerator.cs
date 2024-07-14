namespace MappingTest
{
    public class PairGenerator
    {
        public static List<ClassPair> GeneratePairs(Dictionary<string, ClassInfo> oldClasses, Dictionary<string, ClassInfo> newClasses)
        {
            var pairs = new List<ClassPair>();

            foreach (var oldClass in oldClasses.Values)
            {
                foreach (var newClass in newClasses.Values)
                {
                    pairs.Add(new ClassPair { OldClass = oldClass, NewClass = newClass });
                }
            }

            return pairs;
        }
    }
}
