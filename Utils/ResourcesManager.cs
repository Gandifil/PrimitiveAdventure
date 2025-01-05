using System.Resources;

namespace PrimitiveAdventure.Utils;

public class ResourcesManager
{
    private readonly Dictionary<string, object> _resources = new();
    
    public T Load<T>(string resourceName)
    {
        if (_resources.TryGetValue(resourceName, out var value))
            return (T)value;
        
        T result;
        switch (typeof(T))
        {
            case { } t when t == typeof(IEnumerable<string>): 
                result = (T)LoadTxt(resourceName);
                break;
            default:
                throw new ArgumentException($"Resource type {typeof(T).FullName} is not supported");
        }

        _resources[resourceName] = result!;
        return result;
    }

    private IEnumerable<string> LoadTxt(string resourceName) =>
        File.ReadLines("Resources/" + resourceName + ".txt");
}