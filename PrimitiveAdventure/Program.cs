using PrimitiveAdventure.Core;
using PrimitiveAdventure.Core.Global;
using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Actors;
using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.Screens;
using PrimitiveAdventure.Screens.Fight;
using SadConsole.Configuration;
using Game = SadConsole.Game;
using Settings = SadConsole.Settings;

Settings.WindowTitle = "Primitive Adventure";

Builder gameStartup = new Builder()
    .SetScreenSize(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT)
    .IsStartingScreenFocused(true)
    .ConfigureFonts("Resources/Fonts/Cheepicus12.font")
    .OnStart(Startup);

Game.Create(gameStartup);
Game.Instance.Run();
Game.Instance.Dispose();

void Startup(object? sender, GameHost host)
{
    Settings.ResizeMode = Settings.WindowResizeOptions.Scale;

    if (args.Contains("--test"))
    {
        var player = new Player();
        var level = args.FirstOrDefault(x => x.StartsWith("--level="));
        if (level is not null)
        {
            var value = level.Split('=')[1];
            player.LevelUp(int.Parse(value));
        }
        
        new GameState(GlobalMap.TestMap(), player).StartScreen();
    }
    else if (args.Contains("--test-fight"))
        new FightScreen(new FightProcess(new Player(), [new Dog()])).Start();
    else
        new MainMenu().Start();
}
