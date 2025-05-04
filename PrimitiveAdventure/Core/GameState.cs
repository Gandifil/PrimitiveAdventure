using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Drawing;
using PrimitiveAdventure.Core.Global;
using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.Screens;
using Point = SadRogue.Primitives.Point;
using Rectangle = SadRogue.Primitives.Rectangle;

namespace PrimitiveAdventure.Core;

public class GameState
{
    public static GameState Instance;
    
    public Player Player { get; }
    
    public PlayerCell PlayerCell { get; }

    public GlobalMap GlobalMap { get; }

    public GameState(GlobalMap globalMap, Player player)
    {
        Instance = this;
        GlobalMap = globalMap;
        
        Player = player;
        PlayerCell = new PlayerCell(Player);
        PlayerCell.Position = new Point(3, 3);
        GlobalMap.Spawn(PlayerCell, 3, 3);
    }

    public void MovePlayer(Point shift)
    {
        var newPosition = PlayerCell.Position + shift;

        if (new Rectangle(Point.Zero, GlobalMap.Size - (1, 1)).Contains(newPosition))
        {
            var nextCell = GlobalMap[newPosition];
        
            GlobalMap.Move(PlayerCell.Position, newPosition);
            PlayerCell.Position = newPosition;
            nextCell?.OnCollisionWith(Player);
        }
    }

    public void StartScreen()
    {
        if (Player.Masteries.Count() < Player.MasteryPoints)
        {
            var screen = new MasteryChooseScreen(Player.Masteries.GetFreeMastery())
            {
                Title = "Выберите мастерство для изучения"
            };
            screen.SelectedSuccessfully += mastery =>
            {
                Player.Masteries.Add(mastery);
                StartScreen();
            };
            screen.Start();
            return;
        }
        
        if (Player.Masteries.AllLevels < Player.Level)
        {
            var mscreen = new MasteryChooseScreen(Player.Masteries.Select(x => x.Mastery).ToList())
            {
                Title = "Выберите мастерство для получения таланта"
            };
            mscreen.SelectedSuccessfully += mastery =>
            {
                var handler = Player.Masteries.First(x => x.Mastery == mastery);
                var screen = new TalentChooseScreen(handler.TalentHandlers.Where(x => x.CanUpgrade).ToList())
                {
                    Title = "Выберите талант для изучения",
                    BackScreen = mscreen,
                };
                screen.SelectedSuccessfully += talent =>
                {
                    Debug.Assert(talent.CanUpgrade);
                    talent.Upgrade(Player);
                    StartScreen();
                };
                screen.Start();
            };
            mscreen.Start();
            return;
        }
            
        new GlobalModeScreen(this).Start();
    }
}