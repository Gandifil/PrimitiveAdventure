using PrimitiveAdventure.Core.Rpg.Modifiers;

namespace PrimitiveAdventure.Core.Rpg.Masteries;

public interface ITalent
{
    string Name { get; }
    
    string Description { get; }
    
    int MaxLevel { get; }

    ModifiersList GetModifiersList(int level);
}