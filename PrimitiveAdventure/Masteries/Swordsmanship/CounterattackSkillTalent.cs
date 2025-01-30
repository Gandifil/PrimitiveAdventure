using PrimitiveAdventure.Core.Rpg.Masteries;
using PrimitiveAdventure.Core.Rpg.Modifiers;

namespace PrimitiveAdventure.Masteries.Swordsmanship;

public class CounterattackSkillTalent: ITalent
{
    public string Name => "Counterattack";
    public string Description => "Counterattack skill talent";
    public int MaxLevel => 3;
    public ModifiersList GetModifiersList(int level) => new ModifiersList();
}