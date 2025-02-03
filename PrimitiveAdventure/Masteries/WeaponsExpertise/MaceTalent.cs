using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Masteries;
using PrimitiveAdventure.Core.Rpg.Modifiers;

namespace PrimitiveAdventure.Masteries.WeaponsExpertise;

public class MaceTalent: ITalent
{
    public string Name => "Владение палицей";
    public string Description => "[c:r f:Yellow]Тяжесть - ваша добродетель[c:u 1]";
    public int MaxLevel => 1;

    public ModifiersList GetModifiersList(int level) => new()
    {
        new ParamMod(Parameters.Kind.ArmorPenetration, 5),
        new ParamMod(Parameters.Kind.AttackDamage, 5),
    };
}