using SadConsole.UI;

namespace PrimitiveAdventure.SadConsole.Controls;

public static class ControlHostExtensions
{
    public static ControlCursor GetControlCursor(this ControlHost controlHost)
        => new ControlCursor(controlHost);
}