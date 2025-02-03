using PrimitiveAdventure.Core.Rpg.Modifiers;

namespace PrimitiveAdventure.Core.Rpg.Masteries;

public class TalentHandler
{
    public readonly ITalent Talent;

    public int Level { get; private set; }
    
    public TalentHandler(ITalent talent, int level = 0)
    {
        Talent = talent;
        Level = level;
    }
    
    public bool CanUpgrade => Level < Talent.MaxLevel;

    public ModifiersList CurrentModifiersList => Talent.GetModifiersList(Level);
    
    public ModifiersList NextModifiersList => Talent.GetModifiersList(Level + 1);

    public void Upgrade(Player player)
    {
        Level++;
        CurrentModifiersList.Assign(player);
    }

    public override string ToString()
    {
        return Talent.Name.Prepare();
    }
}