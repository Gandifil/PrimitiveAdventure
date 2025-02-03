using System.Diagnostics;
using PrimitiveAdventure.Core.Global;
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
                var screen = new TalentChooseScreen(handler.TalentHandlers)
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