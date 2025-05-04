using PrimitiveAdventure.Core;
using PrimitiveAdventure.Screens.Views;
using PrimitiveAdventure.Utils;
using SadConsole.UI;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Ui.Controls;

public class PlayerPanel: ActorPanel
{
    private readonly IPlayer _player;

    public PlayerPanel(int width, int height, IPlayer player) : base(width, height, player)
    {
        _player = player;
        // var area = new DrawingArea(5, 5);
        // area.OnDraw += DrawingAreaOnDraw;
        // Controls.Add(area);
        
        Children.Add(new ResourceView(5, 6, Player.RESOURCE_NAME)
        {
            Foreground = Color.Green,
        });
        
        Colors newColors = Colors.Default.Clone();
        newColors.ControlForegroundNormal.SetColor(Color.Green);
        newColors.RebuildAppearances();
        
        Controls.ThemeColors = newColors;
    }
}