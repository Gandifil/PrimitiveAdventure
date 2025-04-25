using System.Reflection;

namespace PrimitiveAdventure.Utils;

public class TypeSearcher
{
    public required Assembly[] Assemblies { get; init; }

    public Type? Search<TBase>(string name)
    {
        return Assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .FirstOrDefault(type => type.IsAssignableTo(typeof(TBase)) && !type.IsAbstract);
    }

    public TBase? SearchAndActivate<TBase>(string name) where TBase: class
    {
        var type = Search<TBase>(name);
        return type is null ? null : Activator.CreateInstance(type) as TBase;
    }

    public static TypeSearcher ForExecutingAssembly() => new TypeSearcher()
    {
        Assemblies = [Assembly.GetExecutingAssembly()],
    };
}