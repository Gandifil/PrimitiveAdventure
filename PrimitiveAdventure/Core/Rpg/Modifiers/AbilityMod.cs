using PrimitiveAdventure.Core.Rpg.Abilities;

namespace PrimitiveAdventure.Core.Rpg.Modifiers;

public class AbilityMod(Ability Ability): IModifier
{
    public string Line => @$"дает способность '{Ability.Name}'";
    
    public void Assign(Actor actor)
    {
        actor.Abilities.Add(Ability);
        Ability.SetOwner(actor);
    }

    public void Cancel(Actor actor)
    {
        actor.Abilities.Remove(Ability);
    }
}