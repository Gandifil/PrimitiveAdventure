using PrimitiveAdventure.Core.Rpg.Abilities;
using PrimitiveAdventure.Core.Rpg.Modifiers;

namespace PrimitiveAdventure.Core.Rpg.Masteries;

public class AbilityTalent<TAbility>: ITalent where TAbility: Ability, new()
{
    public string Name => _ability.Name;
    public string Description  => _ability.Description;
    public int MaxLevel => 1;
    
    private TAbility _ability;

    public AbilityTalent()
    {
        _ability = new TAbility();
    }
    
    public ModifiersList GetModifiersList(int level)
    {
        return new ModifiersList()
        {
            new AbilityMod(_ability)
        };
    }
}