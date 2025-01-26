using PrimitiveAdventure.Core.Rpg.Modifiers;

namespace PrimitiveAdventure.Core.Rpg.Items;

public interface IItem: IHasModifiers
{
    string Name { get; }
    Equipment.Kind Kind { get; }
    string ResourceName { get; }
}