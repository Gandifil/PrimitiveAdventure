namespace PrimitiveAdventure.Core.Rpg.Controlling;

public interface IMove
{
    int Order { get; }
    string DisplayText { get; }
    void Apply(FightProcess fightProcess, Actor actor);
}