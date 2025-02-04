using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Masteries;
using PrimitiveAdventure.Core.Rpg.Modifiers;

namespace PrimitiveAdventure.Masteries.HandToHandCombat;

public class SteelArmsTalent: ITalent
{
    public string Name => "Стальные кулаки";
    public string Description => "[c:r f:Yellow]Монах не ломает камни. Он заставляет их бояться его тишины[c:u 1]";
    public int MaxLevel => 1;

    public ModifiersList GetModifiersList(int level) => new()
    {
        new ParamMod(Parameters.Kind.Armor, 1),
        new ParamMod(Parameters.Kind.AttackDamage, 1),
        new ParamMod(Parameters.Kind.ArmorPenetration, 1),
    };
}