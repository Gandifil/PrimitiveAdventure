namespace PrimitiveAdventure.Core;

public class GameState
{
    public Player Player { get; }

    public GlobalMap GlobalMap { get; }

    public GameState()
    {
        GlobalMap = new GlobalMap(new Point(10, 10));
        GlobalMap.Spawn(new Chest(), 1, 1);
        GlobalMap.Spawn(new Chest(), 4, 4);
        
        Player = new Player();
        Player.GlobalPosition = new Point(5, 5);
    }
}