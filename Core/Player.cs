using PrimitiveAdventure.Core.Global;
using PrimitiveAdventure.Core.Rpg;

namespace PrimitiveAdventure.Core;

public interface IPlayer: IActor, IGlobalMapCell
{
    public Point GlobalPosition { get; }
}

public class Player: Actor, IPlayer
{
    public Point GlobalPosition { get; set; }
    public string Name { get; } = "player";
    public string? Resource { get; } = "player";
}