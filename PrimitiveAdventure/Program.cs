using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.Screens;
using PrimitiveAdventure.Utils;
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

    if (args.Length != 0)
    {
        new CmdParser(args).Run();
        return;
    }

    new MainMenu().Start();
}
