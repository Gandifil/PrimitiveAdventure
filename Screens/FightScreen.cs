using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Controlling;
using PrimitiveAdventure.Ui;
using PrimitiveAdventure.Ui.Controls;
using PrimitiveAdventure.Utils;
using SadConsole.Input;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Screens;

public class FightScreen: BaseScreen
{
    const int CELL_WIDTH = 20;
    const int CELL_HEIGHT = 8;
    
    private readonly FightProcess _fightProcess;
    private readonly Button _attackButton;
    private readonly Button _moveButton;
    private readonly Dictionary<IActor, Panel> _enemyPanels  = new();
    private readonly PlayerPanel _playerPanel;
    
    public FightScreen(int width, int height, FightProcess fightProcess) : base(width, height)
    {
        _fightProcess = fightProcess;

        _attackButton = new KeyedButton("атака".Prepare(), Keys.A);
        _attackButton.Position = new Point(0, height - 1);
        _attackButton.Click += (_, __) => _fightProcess.Player.Control.SetMove(new Attack((Actor)GetAttackTarget()));
        Controls.Add(_attackButton);
        
        _moveButton = new KeyedButton(string.Empty, Keys.Right)
        {
            ShowEnds = false,
        };
        _moveButton.Click += (_, __) => _fightProcess.Player.Control.SetMove(Movement.Right);

        _playerPanel = new PlayerPanel(width: CELL_WIDTH - 1, height: CELL_HEIGHT - 1, _fightProcess.Player);
        Controls.Add(_playerPanel);
        foreach (var enemy in _fightProcess.Team2)
        {
            var panel = new EnemyPanel(width: CELL_WIDTH - 1, height: CELL_HEIGHT - 1, enemy);
            _enemyPanels.Add(enemy, panel);
            Controls.Add(panel);
        }
        
        Update();
            // this.SetEffect(Surface.GetCells(Surface.Area), new Blinker()
            // {
            //     // Blink forever
            //     Duration = System.TimeSpan.MaxValue,
            //     BlinkOutForegroundColor = Color.Black,
            //     // Every half a second
            //     BlinkSpeed = TimeSpan.FromMilliseconds(500),
            //     RunEffectOnApply = true
            // });
    }

    private IActor? GetAttackTarget()
    {
        return _fightProcess.Team2.SingleOrDefault(x =>
            x.LocalPosition.X == _fightProcess.Player.LocalPosition.X + 1);
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
        foreach (var enemy in _fightProcess.Team2)
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
        var rect = new Rectangle(CELL_WIDTH * x + 1, CELL_HEIGHT * y + 1, CELL_WIDTH + 1, CELL_HEIGHT + 1);
        _playerPanel.Position = rect.Position;

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

        _attackButton.IsEnabled = GetAttackTarget() is not null;
    }
}