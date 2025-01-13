using PrimitiveAdventure.Core;
using PrimitiveAdventure.Utils;
using SadConsole.Components;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Ui.Controls;

public class PlayerPanel: ActorPanel
{
    private readonly IPlayer _player;

    public PlayerPanel(int width, int height, IPlayer player) : base(width, height, player)
    {
        _player = player;
        var area = new DrawingArea(5, 5);
        area.OnDraw += DrawingAreaOnDraw;
        Controls.Add(area);
    }

    private void DrawingAreaOnDraw(DrawingArea area, TimeSpan timeSpan)
    {
        var lines = Services.Resources.Load<IEnumerable<string>>(_player.Resource!);
        int i = 0;
        foreach (var line in lines)
        {
            Surface.Print(1, i++, line);
        }
    }
}