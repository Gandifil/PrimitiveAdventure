using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.SadConsole.Controls;
using SadConsole.UI;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Ui.Controls;

public class EnemyPanel: ActorPanel
{
    private Label _moveLabel;
    public EnemyPanel(int width, int height, IActor actor) : base(width, height, actor)
    {
        _moveLabel = new Label(actor.Controller.Move.DisplayText)
        {
            AlternateFont = Game.Instance.LoadFont("Resources/Fonts/Cheepicus12_special.font"),
            
        };

        actor.Controller.Changed += (_, _) => _moveLabel.DisplayText = actor.Controller.Move.DisplayText;
            
        Controls.GetControlCursor()
            .PrintLine(new Label(Actor.Name.Prepare()))
            .PrintLine(_moveLabel);
        
        Colors newColors = Colors.Default.Clone();
        newColors.ControlForegroundNormal.SetColor(Color.Red);
        newColors.RebuildAppearances();
        
        Controls.ThemeColors = newColors;
    }
}