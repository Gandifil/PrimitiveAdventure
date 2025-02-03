using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Masteries;
using PrimitiveAdventure.Core.Rpg.Modifiers;

namespace PrimitiveAdventure.Masteries.WeaponsExpertise;

public class CounterattackSkillTalent: ITalent
{
    public string Name => "Владение мечом";
    public string Description => "[c:r f:Yellow]Стань воплощением клинка, где сталь и дух — едины[c:u 1]";
    public int MaxLevel => 1;

    public ModifiersList GetModifiersList(int level) => new()
    {
        new ParamMod(Parameters.Kind.CounterAttackRate, 10),
        new ParamMod(Parameters.Kind.AttackDamage, 4),
        new ParamMod(Parameters.Kind.ArmorDefenceBonus, 5),
    };
}