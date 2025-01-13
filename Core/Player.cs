using PrimitiveAdventure.Core.Global;
using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Controlling;

namespace PrimitiveAdventure.Core;

public interface IPlayer: IActor, IGlobalMapCell
{
    public Point GlobalPosition { get; }
    
    public IActorController Control { get; }
}

public class Player: Actor, IPlayer
{
    public Point GlobalPosition { get; set; }
    public IActorController Control => (PlayerController)Controller;
    public string Name { get; } = "player";
    public string? Resource { get; } = "player";

    public Player()
    {
        Direction = 1;
    }
}