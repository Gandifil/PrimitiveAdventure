using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Ui;

public static class PanelExtensions
{
    public static void AutoAdd(this Panel panel, ControlBase control)
    {
        control.Position = (0, panel.Sum(x => x.Height));
        panel.Add(control);
    }
}