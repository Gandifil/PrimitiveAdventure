using PrimitiveAdventure.Core;
using PrimitiveAdventure.Core.Global;
using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Actors;
using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.Screens;
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

    if (args.Contains("-test"))
        new GameState(GlobalMap.TestMap(), new Player()).StartScreen();
    else if (args.Contains("--test-fight"))
        new FightScreen(new FightProcess(new Player(), [new Dog()])).Start();
    else
        new MainMenu().Start();
}
