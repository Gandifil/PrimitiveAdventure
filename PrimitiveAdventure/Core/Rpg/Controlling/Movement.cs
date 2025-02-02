using PrimitiveAdventure.Screens;

namespace PrimitiveAdventure.Core.Rpg.Controlling;

public class Movement(int Direction): IMove
{
    public int Order => 5;
    public string DisplayText => Direction == -1 ? "<--" : "-->";
    
    public void Apply(FightProcess fightProcess, Actor actor)
    {
        actor.LocalPosition += (Direction, 0);
        (Game.Instance.Screen as FightScreen).Update();
    }
    
    public static readonly Movement Left = new(-1);
    public static readonly Movement Right = new(1);
}