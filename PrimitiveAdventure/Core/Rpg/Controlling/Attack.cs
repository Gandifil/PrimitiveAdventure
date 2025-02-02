using PrimitiveAdventure.Screens;

namespace PrimitiveAdventure.Core.Rpg.Controlling;

public record Attack(Actor Target): IMove
{
    public int Order => 5;
    
    public string DisplayText { get; }
    
    public void Apply(FightProcess fightProcess, Actor actor)
    {
        actor.Attack(Target);
    }
}