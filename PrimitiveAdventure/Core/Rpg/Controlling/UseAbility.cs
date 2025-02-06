using PrimitiveAdventure.Core.Rpg.Abilities;
using PrimitiveAdventure.Core.Rpg.Fight;

namespace PrimitiveAdventure.Core.Rpg.Controlling;

public class UseAbility(IAbility Ability, IActor? Target = null): IMove
{
    public int Order => 5;
    public string DisplayText => "*";
    public void Apply(FightProcess fightProcess, Actor actor)
    {
        var ability = (Ability)Ability;
        ability.Use((Actor?)Target);
    }
}