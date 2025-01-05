namespace PrimitiveAdventure.Core.Rpg;

public interface IActor
{
    string Name { get; }
    
    Point LocalPosition { get; }
}

public class Actor : IActor
{
    public string Name { get; protected set; }
    public Point LocalPosition { get; set; }
}