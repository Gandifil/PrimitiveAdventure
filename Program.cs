using PrimitiveAdventure.Core;
using PrimitiveAdventure.Screens;
using SadConsole.Configuration;
using Game = SadConsole.Game;
using Settings = SadConsole.Settings;

Settings.WindowTitle = "Primitive Adventure";

Builder gameStartup = new Builder()
    .SetScreenSize(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT)
    //.SetStartingScreen<MainMenu>()
    .UseDefaultConsole()
    .IsStartingScreenFocused(true)
    .ConfigureFonts(true)
    .ConfigureFonts("Resources/Fonts/Cheepicus12.font")
    .OnStart(Startup);

Game.Create(gameStartup);
Game.Instance.Run();
Game.Instance.Dispose();

void Startup(object? sender, GameHost host)
{
    Settings.ResizeMode = Settings.WindowResizeOptions.Scale;

    Game.Instance.Screen = new GlobalModeScreen(new GameState());
    Game.Instance.Screen.IsFocused = true;
    Game.Instance.DestroyDefaultStartingConsole();
}
