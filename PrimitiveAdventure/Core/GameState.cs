using PrimitiveAdventure.Core.Global;
using PrimitiveAdventure.Core.Rpg.Actors;
using PrimitiveAdventure.Core.Rpg.Items;
using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.Screens;

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
        GlobalMap.Spawn(new ItemCell(new Sword()), 3, 4);
        var enemyGroup = new EnemyGroup();
        enemyGroup.Enemies.Add(new Dog());
        GlobalMap.Spawn(enemyGroup, 2, 3);
        
        Player = new Player();
        Player.GlobalPosition = new Point(3, 3);
        GlobalMap.Spawn(Player, 3, 3);
    }

    public void MovePlayer(Point shift)
    {
        var newPosition = Player.GlobalPosition + shift;
        var nextCell = GlobalMap[newPosition];
        
        GlobalMap.Move(Player.GlobalPosition, newPosition);
        Player.GlobalPosition = newPosition;
        nextCell?.OnCollisionWith(Player);
    }

    public void StartScreen()
    {
        var global = new GlobalModeScreen(this);
        
        var sMasteryChooseScreen = new MasteryChooseScreen(Player.Masteries)
        {
            NextScreen = global,
        };
        
        var masteryChooseScreen = new MasteryChooseScreen(Player.Masteries)
        {
            NextScreen = sMasteryChooseScreen,
        };
        
        masteryChooseScreen.Start();
    }
}