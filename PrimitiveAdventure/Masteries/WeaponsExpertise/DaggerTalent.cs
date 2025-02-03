using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Masteries;
using PrimitiveAdventure.Core.Rpg.Modifiers;

namespace PrimitiveAdventure.Masteries.WeaponsExpertise;

public class DaggerTalent: ITalent
{
    public string Name => "Владение кинжалом";
    public string Description => "[c:r f:Yellow]Скорость - ваша религия[c:u 1]";
    public int MaxLevel => 1;

    public ModifiersList GetModifiersList(int level) => new()
    {
        new ParamMod(Parameters.Kind.RepeatAttackRate, 10),
        new ParamMod(Parameters.Kind.CriticalRate, 10),
        new ParamMod(Parameters.Kind.CriticalDamage, 100),
    };
}