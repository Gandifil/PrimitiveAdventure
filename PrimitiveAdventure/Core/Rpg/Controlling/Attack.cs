using PrimitiveAdventure.Screens;

namespace PrimitiveAdventure.Core.Rpg.Controlling;

public record Attack(Actor Target): IMove
{
    public string DisplayText { get; }
    public void Apply(FightProcess fightProcess, Actor actor)
    {
        actor.Attack(Target);
        //(Game.Instance.Screen as FightScreen).Update();
    }
}