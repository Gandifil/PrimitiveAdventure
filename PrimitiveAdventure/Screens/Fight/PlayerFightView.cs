using PrimitiveAdventure.Core;
using PrimitiveAdventure.SadConsole.Controls;
using PrimitiveAdventure.Ui.Controls;
using SadConsole.UI;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Screens.Fight;

public class PlayerFightView: ControlsConsole
{
    public PlayerFightView(int width, int height, IPlayer player) : base(width, height)
    {
        
        var magicBar = new MyProgressBar(width, 1, HorizontalAlignment.Left)
        {
            Position = (0, 0),
            Progress = player.Magic.Progress,
            BarColor = Color.Blue,
        };
        player.Magic.Changed += () => magicBar.Progress = player.Magic.Progress;
        Controls.Add(magicBar);

        var staminaBar = new MyProgressBar(width, 1, HorizontalAlignment.Left)
        {
            Position = (0, 1),
            Progress = player.Stamina.Progress,
            BarColor = Color.YellowGreen,
        };
        player.Stamina.Changed += () => staminaBar.Progress = player.Stamina.Progress;

        Controls.GetControlCursor()
            .PrintLine(new Label(player.Name.Prepare()))
            .PrintLine(new Label("Магия".Prepare()))
            .PrintLine(magicBar)
            .PrintLine(new Label("Запас сил".Prepare()))
            .PrintLine(staminaBar);
    }
}