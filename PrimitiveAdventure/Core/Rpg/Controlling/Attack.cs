using PrimitiveAdventure.Core.Rpg.Fight;
using PrimitiveAdventure.Screens;

namespace PrimitiveAdventure.Core.Rpg.Controlling;

public record Attack(IActor Target): IMove
{
    public int Order => 5;
    
    public string DisplayText { get; }
    
    public void Apply(FightProcess fightProcess, Actor actor)
    {
        actor.Attack((Actor)Target);
    }
}