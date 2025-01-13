using System.Net.NetworkInformation;
using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Ui.Controls;
using SadConsole.Effects;
using SadConsole.UI;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Ui;

public class EnemyPanel: Panel
{
    private readonly IActor _enemy;
    
    public EnemyPanel(int width, int height, IActor actor) : base(width, height)
    {
        _enemy = actor;
        
        Add(new Label(_enemy.Name.Prepare()));
        // Add(new Controls.ProgressBar(width, 1, _enemy.Health)
        // {
        //     Position = (0, height - 1)
        // });  
        var pb = new MyProgressBar(width, 1, HorizontalAlignment.Left)
        {
            Position = (0, height - 1),
            Progress = actor.Health.Progress,
        };
        Add(pb);

        actor.Health.Changed += () => pb.Progress = actor.Health.Progress;
        
        Colors newColors = Colors.Default.Clone();
        newColors.ControlForegroundNormal.SetColor(Color.Red);
        newColors.RebuildAppearances();
        SetThemeColors(newColors);
        Controls.ForEach(x => x.SetThemeColors(newColors));
    }
}