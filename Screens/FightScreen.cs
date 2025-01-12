using System.Diagnostics;
using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Controlling;
using PrimitiveAdventure.Ui;
using PrimitiveAdventure.Ui.Controls;
using PrimitiveAdventure.Utils;
using SadConsole.Input;
using SadConsole.UI.Controls;
using ProgressBar = PrimitiveAdventure.Ui.Controls.ProgressBar;

namespace PrimitiveAdventure.Screens;

public class FightScreen: BaseScreen
{
    const int CELL_WIDTH = 20;
    const int CELL_HEIGHT = 8;
    
    private readonly FightProcess _fightProcess;
    private readonly Button _button;
    private readonly Button _moveButton;
    private readonly Dictionary<IActor, Panel> _enemyPanels  = new();
    
    public FightScreen(int width, int height, FightProcess fightProcess) : base(width, height)
    {
        _fightProcess = fightProcess;

        _button = new KeyedButton("атака".Prepare(), Keys.A);
        _button.Position = new Point(0, height - 1);
        _button.Click += (_, __) => Debug.Assert(false);
        Controls.Add(_button);
        
        
        _moveButton = new KeyedButton(string.Empty, Keys.Right)
        {
            ShowEnds = false,
        };
        _moveButton.Click += (_, __) => _fightProcess.Player.Control.SetMove(Movement.Right);
        
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

        var movePosition = _fightProcess.Player.LocalPosition + (1, 0);
        
        _moveButton.IsEnabled = false;
        Controls.Remove(_moveButton);
        if (!_fightProcess.All.Any(x => x.LocalPosition.X == movePosition.X))
        {
            var move = new Rectangle(CELL_WIDTH * movePosition.X, CELL_HEIGHT * movePosition.Y,
                CELL_WIDTH + 1, CELL_HEIGHT + 1);

            _moveButton.Position = new Point(move.X + 1, move.Y + 1);
            _moveButton.IsEnabled = true;
            Controls.Add(_moveButton);
        }
    }
}