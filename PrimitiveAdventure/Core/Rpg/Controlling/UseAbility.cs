using PrimitiveAdventure.Core.Rpg.Abilities;

namespace PrimitiveAdventure.Core.Rpg.Controlling;

public class UseAbility(IAbility ability): IMove
{
    public string DisplayText => "*";
    public void Apply(FightProcess fightProcess, Actor actor)
    {
        throw new NotImplementedException();
    }
}