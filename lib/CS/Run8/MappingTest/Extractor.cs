namespace MappingTest
{
    public class Extractor
    {
        public static Dictionary<string, ClassInfo> GetClassesFromAssembly(string assemblyPath)
        {
            var classes = new Dictionary<string, ClassInfo>();
            var assembly = Mono.Cecil.AssemblyDefinition.ReadAssembly(assemblyPath);

            foreach (var module in assembly.Modules)
            {
                foreach (var type in module.Types)
                {
                    if (type.IsClass && type.FullName.StartsWith("ns0"))
                    {
                        var classInfo = new ClassInfo
                        {
                            ClassName = type.FullName,
                            Methods = type.Methods.Select(m => new MethodInfo
                            {
                                Name = m.Name,
                                Parameters = m.Parameters.Select(p => p.ParameterType.FullName).ToList(),
                                ReturnType = m.ReturnType.FullName,
                                Body = m.HasBody ? string.Join(" ", m.Body.Instructions.Select(i => i.OpCode.Code.ToString())) : string.Empty
                            }).ToList(),
                            Fields = type.Fields.Select(f => f.Name).ToList(),
                            Properties = type.Properties.Select(p => p.Name).ToList()
                        };
                        classes[classInfo.ClassName] = classInfo;
                    }
                }
            }

            return classes;
        }
    }
}
