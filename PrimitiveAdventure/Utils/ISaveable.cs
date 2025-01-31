namespace PrimitiveAdventure.Utils;

public interface ISaveable
{
    void Save(StreamWriter streamWriter);

    void Load(StreamReader streamReader);
}