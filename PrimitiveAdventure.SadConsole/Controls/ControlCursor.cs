using SadConsole.UI;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.SadConsole.Controls;

public class ControlCursor(ControlHost ControlHost)
{
    public Point Position { get; set; }

    public int Shift { get; set; } = 0;

    public ControlCursor ToStart()
    {
        Position = new Point(0, 0);
        return this;
    }

    public ControlCursor Print(ControlBase control)
    {
        control.Position = Position;
        Position += (0, control.Height + Shift);
        ControlHost.Add(control);
        return this;
    }

    public ControlCursor SetShift(int value)
    {
        Shift = value;
        return this;
    }

    public ControlCursor NewLine()
    {
        Position += (0, 1);
        return this;
    }
}