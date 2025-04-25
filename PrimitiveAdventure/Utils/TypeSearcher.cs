using System.Reflection;

namespace PrimitiveAdventure.Utils;

public class TypeSearcher
{
    public required Assembly[] Assemblies { get; init; }

    public Type? Search<TBase>(string name)
    {
        return Assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .FirstOrDefault(type => type.IsAssignableTo(typeof(TBase)) 
                                    && !type.IsAbstract 
                                    && string.Equals(type.Name ,name, StringComparison.OrdinalIgnoreCase));
    }

    public TBase? SearchAndActivate<TBase>(string name, params object?[]? args) where TBase: class
    {
        var type = Search<TBase>(name);
        return type is null ? null : Activator.CreateInstance(type, args) as TBase;
    }

    public static TypeSearcher ForExecutingAssembly() => new TypeSearcher()
    {
        Assemblies = [Assembly.GetExecutingAssembly()],
    };
}