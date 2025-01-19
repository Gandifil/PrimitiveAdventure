namespace PrimitiveAdventure.Core.Rpg.Items;

public interface IItem
{
    string Name { get; }
    Equipment.Kind Kind { get; }
    string ResourceName { get; }
}