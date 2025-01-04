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
        GlobalMap.Spawn(new EnemyGroup(), 2, 3);
        
        Player = new Player();
        Player.GlobalPosition = new Point(3, 3);
        GlobalMap.Spawn(Player, 3, 3);
    }

    public void MovePlayer(Point shift)
    {
        GlobalMap.Move(Player.GlobalPosition, Player.GlobalPosition + shift);
        Player.GlobalPosition += shift;
    }
}