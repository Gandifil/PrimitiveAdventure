using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.Ui.Controls;
using SadConsole.UI;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Ui;

public class EnemyPanel: ActorPanel
{
    public EnemyPanel(int width, int height, IActor actor) : base(width, height, actor)
    {
        Add(new Label(Actor.Name.Prepare()));
        
        Colors newColors = Colors.Default.Clone();
        newColors.ControlForegroundNormal.SetColor(Color.Red);
        newColors.RebuildAppearances();
        this.SetThemeColorsForControls(newColors);
    }
}