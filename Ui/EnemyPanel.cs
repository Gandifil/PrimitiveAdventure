using PrimitiveAdventure.Core.Rpg;
using SadConsole.UI;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Ui;

public class EnemyPanel: Panel
{
    private readonly IActor _enemy;
    
    public EnemyPanel(int width, int height, IActor actor) : base(width, height)
    {
        _enemy = actor;
        Colors newColors = Colors.Default.Clone();

        newColors.ControlForegroundNormal.SetColor(Color.Red);
        newColors.RebuildAppearances();
        SetThemeColors(newColors);
        
        Add(new Label(_enemy.Name.Prepare()));
        Add(new Controls.ProgressBar(width, 1, _enemy.Health)
        {
            Position = (0, height - 1)
        });
        
        Controls.ForEach(x => x.SetThemeColors(newColors));
    }
}