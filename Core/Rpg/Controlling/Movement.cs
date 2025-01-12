using PrimitiveAdventure.Screens;

namespace PrimitiveAdventure.Core.Rpg.Controlling;

public class Movement: IMove
{
    public string DisplayText => Direction ? "<--" : "-->";

    public bool Direction { get; }

    public Movement(bool direction)
    {
        Direction = direction;
    }
    
    public void Apply(FightProcess fightProcess, Actor actor)
    {
        actor.LocalPosition += (Direction ? -1 : 1, 0);
        (Game.Instance.Screen as FightScreen).Update();
    }
    
    public static readonly Movement Left = new(true);
    public static readonly Movement Right = new(false);
}