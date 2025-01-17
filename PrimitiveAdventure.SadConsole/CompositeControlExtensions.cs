using SadConsole.UI;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.SadConsole;

public static class CompositeControlExtensions
{
    public static void SetThemeColorsForControls(this CompositeControl container, Colors? colors)
    {
        foreach (var control in container)
        {
            control.SetThemeColors(colors);
        }
    }
}