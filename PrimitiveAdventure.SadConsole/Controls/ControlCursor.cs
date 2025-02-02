using SadConsole.UI;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.SadConsole.Controls;

public class ControlCursor(ControlHost ControlHost)
{
    public int Position { get; set; }
    
    public int Line { get; set; }

    public int Shift { get; set; } = 1;

    public ControlCursor ToStart()
    {
        Position = 0;
        Line = 0;
        return this;
    }

    public ControlCursor Print(ControlBase control)
    {
        control.Position = (Position, Line);
        ControlHost.Add(control);
        Position += control.Width;
        return this;
    }

    public ControlCursor PrintLine(ControlBase control) => Print(control).NewLine(Shift);

    public ControlCursor PrintLine(ControlBase control, ControlBase nextControl) =>
        Print(control).PrintLine(nextControl);

    public ControlCursor SetShift(int value)
    {
        Shift = value;
        return this;
    }

    public ControlCursor NewLine(int shift = 1)
    {
        Position = 0;
        Line += shift;
        return this;
    }
}