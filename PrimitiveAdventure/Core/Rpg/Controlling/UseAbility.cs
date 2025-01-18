using PrimitiveAdventure.Core.Rpg.Abilities;

namespace PrimitiveAdventure.Core.Rpg.Controlling;

public class UseAbility(IAbility Ability, IActor? Target = null): IMove
{
    public string DisplayText => "*";
    public void Apply(FightProcess fightProcess, Actor actor)
    {
        var ability = (Ability)Ability;
        ability.Use((Actor?)Target);
    }
}