using System.Diagnostics;
using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Abilities;
using PrimitiveAdventure.Core.Rpg.Controlling;
using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.SadConsole.Controls;
using PrimitiveAdventure.SadConsole.Screens;
using PrimitiveAdventure.Screens.Views;
using PrimitiveAdventure.Ui.Controls;
using SadConsole.Input;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Screens;

public class FightScreen: BaseScreen
{
    const int CELL_WIDTH = 24;
    const int CELL_HEIGHT = 10;
    
    private readonly FightProcess _fightProcess;
    private readonly Button _attackButton;
    private readonly Button _abilitiesButton;
    private readonly Button _moveButton;
    private readonly Dictionary<IActor, EnemyPanel> _enemyPanels  = new();
    private readonly PlayerPanel _playerPanel;
    
    public FightScreen(int width, int height, FightProcess fightProcess) : base(width, height)
    {
        _fightProcess = fightProcess;

        _attackButton = new KeyedButton("атака".Prepare(), Keys.A);
        _attackButton.Position = new Point(0, height - 1);
        _attackButton.Click += (_, __) => _fightProcess.Player.Control.SetMove(new Attack((Actor)GetAttackTarget()));
        Controls.Add(_attackButton);

        _abilitiesButton = new KeyedButton("способности".Prepare(), Keys.S);
        _abilitiesButton.Position = new Point(12, height - 1);
        _abilitiesButton.Click += (_, __) =>
        {
            var ability = new AbilityChooseScreen(_fightProcess.Player.Abilities)
            {
                BackScreen = this,
            };
            ability.SelectedSuccessfully += AbilityOnSelectedSuccessfully;
            ability.Start();
        };
        Controls.Add(_abilitiesButton);
        
        _moveButton = new KeyedButton(string.Empty, Keys.Right)
        {
            ShowEnds = false,
        };
        _moveButton.Click += (_, __) => _fightProcess.Player.Control.SetMove(Movement.Right);

        _playerPanel = new PlayerPanel(width: CELL_WIDTH - 1, height: CELL_HEIGHT - 1, _fightProcess.Player);
        Children.Add(new ActorResourcesView(width, _fightProcess.Player)
        {
            Position = new Point(0, CELL_HEIGHT * FightProcess.MAP_HEIGHT),
        });
        Children.Add(_playerPanel);
        foreach (var enemy in _fightProcess.Team2)
        {
            var panel = new EnemyPanel(width: CELL_WIDTH - 1, height: CELL_HEIGHT - 1, enemy);
            panel.Click += PanelOnClick;
            _enemyPanels.Add(enemy, panel);
            Children.Add(panel);
        }
        
        Update();
        IsDirty = true;
    }

    private void AbilityOnSelectedSuccessfully(IAbility ability)
    {
        this.Start();
        _selectedAbility = ability;
        if (ability.TargetIsRequired)
        {
            //SadComponents.Add(new SelectTargetMode());
            var items = _fightProcess.MapTeam1.AllWhere(ability.TargetKind);
            if (items.Count > 0)
                StartSelectMode(items);
            // else if (items.Count == 1)
            //     _fightProcess.Player.Control.SetMove(new UseAbility(ability, items.First()));
            else
            {
                Debug.Assert(false);
            }
        }
        else
            _fightProcess.Player.Control.SetMove(new UseAbility(ability));
    }
    
    private IAbility _selectedAbility;

    private void StartSelectMode(IReadOnlyCollection<IActor> items)
    {
        foreach (var actor in items)
            if (_enemyPanels.TryGetValue(actor, out var panel))
            {
                panel.CanBeSelected = true;
                panel.CanClick = true;
            }
        
        // TODO: add player or refactor
    }

    private void PanelOnClick(object? sender, EventArgs e)
    {
        var panel = (ActorPanel)sender!;
        _fightProcess.Player.Control.SetMove(new UseAbility(_selectedAbility, panel.Actor));
        StopSelectMode();
    }

    private void StopSelectMode()
    {
        foreach (var (_, panel) in _enemyPanels)
        {
            panel.CanBeSelected = false;
            panel.CanClick = false;
        }
    }

    private IActor? GetAttackTarget()
    {
        return _fightProcess.Team2.SingleOrDefault(x =>
            x.LocalPosition.X == _fightProcess.Player.LocalPosition.X + 1);
    }

    public void Update()
    {
        //Children.Clear();
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