using PrimitiveAdventure.Core.Rpg.Modifiers;

namespace PrimitiveAdventure.Core.Rpg.Masteries;

internal class TalentHandler
{
    public readonly ITalent Talent;

    public int Level { get; }
    
    public TalentHandler(ITalent talent, int level = 0)
    {
        Talent = talent;
        Level = level;
    }
    
    public bool CanUpgrade => Level < Talent.MaxLevel;

    public ModifiersList CurrentModifiersList => Talent.GetModifiersList(Level);
    
    public ModifiersList NextModifiersList => Talent.GetModifiersList(Level + 1);
}