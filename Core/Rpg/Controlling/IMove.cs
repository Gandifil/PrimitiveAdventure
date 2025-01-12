namespace PrimitiveAdventure.Core.Rpg.Controlling;

public interface IMove
{
    string DisplayText { get; }
    void Apply(FightProcess fightProcess, Actor actor);
}