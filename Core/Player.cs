namespace PrimitiveAdventure.Core;

public interface IPlayer: IGlobalMapCell
{
    public Point GlobalPosition { get; }
}

public class Player: IPlayer
{
    public Point GlobalPosition { get; set; }
    public string Name { get; } = "player";
    public string? Resource { get; } = "player";
}