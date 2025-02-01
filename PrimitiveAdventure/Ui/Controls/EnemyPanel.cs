using PrimitiveAdventure.Core.Rpg;
using SadConsole.UI;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Ui.Controls;

public class EnemyPanel: ActorPanel
{
    public EnemyPanel(int width, int height, IActor actor) : base(width, height, actor)
    {
        Controls.Add(new Label(Actor.Name.Prepare()));
        
        Colors newColors = Colors.Default.Clone();
        newColors.ControlForegroundNormal.SetColor(Color.Red);
        newColors.RebuildAppearances();
        
        Controls.ThemeColors = newColors;
    }
}