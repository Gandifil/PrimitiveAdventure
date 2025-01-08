using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Ui;
using PrimitiveAdventure.Utils;
using SadConsole.UI.Controls;
using ProgressBar = PrimitiveAdventure.Ui.Controls.ProgressBar;

namespace PrimitiveAdventure.Screens;

public class FightScreen: BaseScreen
{
    const int CELL_WIDTH = 20;
    const int CELL_HEIGHT = 8;
    
    private readonly FightProcess _fightProcess;
    private readonly Button _button;
    private readonly Dictionary<IActor, Panel> _enemyPanels  = new();
    
    public FightScreen(int width, int height, FightProcess fightProcess) : base(width, height)
    {
        _fightProcess = fightProcess;

        _button = new Button(width: 12, height: 1);
        _button.Position = new Point(0, height - 1);
        _button.Text = "атака [A]".Prepare();
        Controls.Add(_button);
        
        foreach (var enemy in _fightProcess.Enemies)
        {
            var panel = new EnemyPanel(width: CELL_WIDTH - 1, height: CELL_HEIGHT - 1, enemy);
            _enemyPanels.Add(enemy, panel);
            Controls.Add(panel);
        }
        
        Update();
    }

    public void Update()
    {
        Children.Clear();
        this.Clear();

        DrawWalls();
        DrawPlayer();
        DrawEnemies();
    }

    private void DrawEnemies()
    {
        foreach (var enemy in _fightProcess.Enemies)
            DrawEnemy(enemy);
    }

    private void DrawEnemy(IActor enemy)
    {
        var (x, y) = enemy.LocalPosition;
        var rect = new Rectangle(CELL_WIDTH * x, CELL_HEIGHT * y, CELL_WIDTH + 1, CELL_HEIGHT + 1);
        _enemyPanels[enemy].Position = rect.Position + (1, 1);
        //_enemyPanels[enemy].UpdateAndRedraw(TimeSpan.Zero);

        // Cursor.Move(rect.X + 1, rect.Y + 1)
        //     .SetPrintAppearance(Color.Red)
        //     .Print(enemy.Name.Prepare());
    }

    private void DrawWalls()
    {
        for (int x = 0; x < FightProcess.MAP_WIDTH; x++)
        for (int y = 0; y < FightProcess.MAP_HEIGHT; y++)
        {
            var rect = new Rectangle(CELL_WIDTH * x, CELL_HEIGHT * y, CELL_WIDTH + 1, CELL_HEIGHT + 1);
            Surface.DrawBox(rect, ShapeParameters.CreateStyledBoxThin(Color.LightGray));
        }
        Surface.ConnectLines(ICellSurface.ConnectedLineThin);
    }

    private void DrawPlayer()
    {
        var (x, y) = _fightProcess.Player.LocalPosition;
        var rect = new Rectangle(CELL_WIDTH * x, CELL_HEIGHT * y, CELL_WIDTH + 1, CELL_HEIGHT + 1);
        var lines = Services.Resources.Load<IEnumerable<string>>(_fightProcess.Player.Resource!);

        Cursor.Move(rect.X + 1, rect.Y + 1)
            .SetPrintAppearance(Color.Green);
        foreach (var line in lines)
        {
            Cursor.Print(line);
            Cursor.Row++;
            Cursor.Column = rect.X + 1;
        }
    }
}