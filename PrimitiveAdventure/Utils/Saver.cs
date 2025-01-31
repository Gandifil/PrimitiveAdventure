using System.Diagnostics;

namespace PrimitiveAdventure.Utils;

public class Saver<T>(string Path, string DirectoryName, string Extension) where T : class, ISaveable, new()
{
    public T[] LoadAll()
    {
        var fullPath = System.IO.Path.Combine(Path, DirectoryName);
        Directory.CreateDirectory(fullPath);
        var files = Directory.EnumerateFiles(fullPath, "*." + Extension,
            SearchOption.TopDirectoryOnly);
        return files.Select(LoadFrom).ToArray()!;
    }

    public T? LoadFrom(string filePath)
    {
        try
        {
            using var fileStream = new FileStream(filePath, FileMode.Open);
            using var streamReader = new StreamReader(fileStream);
            var entity = new T();
            entity.Load(streamReader);
            return entity;
        }
        catch (Exception e)
        {
            Debug.Print(e.Message);
            return null;
        }
    }
    
    public T? Load(string fileName)
    {
        var filePath = System.IO.Path.Combine(Path, DirectoryName, $"{fileName}.{Extension}");
        return LoadFrom(filePath);
    }

    public void Save(string name, T entity)
    {
        var fullPath = System.IO.Path.Combine(Path, DirectoryName);
        Directory.CreateDirectory(fullPath);
        var filePath = System.IO.Path.Combine(fullPath, $"{name}.{Extension}");
        using var fileStream = new FileStream(filePath, FileMode.Create);
        using var streamWriter = new StreamWriter(fileStream);
        entity.Save(streamWriter);
    }
}