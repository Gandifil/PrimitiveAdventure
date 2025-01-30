using PrimitiveAdventure.Core.Rpg.Masteries;
using PrimitiveAdventure.Core.Rpg.Modifiers;

namespace PrimitiveAdventure.Masteries.HandToHandCombat;

public class SteelArmsTalent: ITalent
{
    public string Name => "Стальные кулаки";
    public string Description => "Стальные кулаки";
    public int MaxLevel => 3;

    public ModifiersList GetModifiersList(int level) => new()
    {
        
    };
}