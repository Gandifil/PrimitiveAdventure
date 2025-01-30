namespace PrimitiveAdventure.SadConsole;

public static class ScreenObjectExtensions
{
    public static void Start(this IScreenObject screen)
    {
        if (Game.Instance.Screen != null)
            Game.Instance.Screen.IsFocused = false;
        Game.Instance.Screen = screen;
        screen.IsFocused = true;
    }
}