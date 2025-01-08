using PrimitiveAdventure.Core.Rpg;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Ui;

public class EnemyPanel: Panel
{
    private readonly IActor _enemy;
    
    public EnemyPanel(int width, int height, IActor actor) : base(width, height)
    {
        _enemy = actor;
        
        this.AutoAdd(new Label(_enemy.Name.Prepare()));
        this.AutoAdd(new Controls.ProgressBar(width, 1, _enemy.Health));
    }
}