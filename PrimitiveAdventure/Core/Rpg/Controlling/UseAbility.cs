using PrimitiveAdventure.Core.Rpg.Abilities;

namespace PrimitiveAdventure.Core.Rpg.Controlling;

public class UseAbility(IAbility ability, IActor? target = null): IMove
{
    public string DisplayText => "*";
    public void Apply(FightProcess fightProcess, Actor actor)
    {
        throw new NotImplementedException();
    }
}