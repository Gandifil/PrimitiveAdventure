namespace PrimitiveAdventure.Core;

public class Player: IGlobalMapCell
{
    public Point GlobalPosition { get; set; }
    public string Name { get; } = "player";
    public string? Resource { get; } = "player";
}