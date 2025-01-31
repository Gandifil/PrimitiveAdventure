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

    public GameState(GlobalMap globalMap, Player player)
    {
        GlobalMap = globalMap;
        
        Player = player;
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
        
        // var sMasteryChooseScreen = new MasteryChooseScreen(Player.Masteries)
        // {
        //     BackScreen = global,
        // };
        //
        // var masteryChooseScreen = new MasteryChooseScreen(Player.Masteries)
        // {
        //     BackScreen = sMasteryChooseScreen,
        // };
        
        global.Start();
    }
}