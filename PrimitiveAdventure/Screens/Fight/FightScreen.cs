using System.Diagnostics;
using PrimitiveAdventure.Core;
using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Abilities;
using PrimitiveAdventure.Core.Rpg.Controlling;
using PrimitiveAdventure.Core.Rpg.Fight;
using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.SadConsole.Controls;
using PrimitiveAdventure.Screens.Base;
using PrimitiveAdventure.Screens.Views;
using PrimitiveAdventure.Ui.Controls;
using SadConsole.Input;
using SadConsole.UI;
using SadConsole.UI.Controls;

namespace PrimitiveAdventure.Screens.Fight;

public class FightScreen: GlobalScreen
{
    const int CELL_WIDTH = 24;
    const int CELL_HEIGHT = 10;
    
    private readonly IFightProcess _fightProcess;
    private readonly FightLog _fightLog;
    private readonly Dictionary<IActor, EnemyPanel> _enemyPanels  = new();
    private readonly PlayerPanel _playerPanel;
    private readonly IPlayer _player;

    private readonly Button _attackButton;
    private readonly Button _defenceButton;
    private readonly Button _abilitiesButton;
    private readonly Button _moveButton;
    
    public FightScreen(IPlayer player, IFightProcess fightProcess)
    {
        _player = player;
        _fightProcess = fightProcess;
        _fightProcess.Team1.Defeated += Team1OnDefeated;
        _fightProcess.Team2.Defeated += Team2OnDefeated;
        
        _attackButton = new KeyedButton("атака".Prepare(), Keys.A);
        _attackButton.Click += (_, __) => _player.Control.SetMove(new Attack((Actor)GetAttackTarget()));
        
        _defenceButton = new KeyedButton("защита".Prepare(), Keys.D);
        _defenceButton.Click += (_, __) => _player.Control.SetMove(new Defence());
        
        _abilitiesButton = new KeyedButton("способности".Prepare(), Keys.S);
        _abilitiesButton.Click += (_, __) =>
        {
            var ability = new AbilityChooseScreen(_player.Abilities)
            {
                BackScreen = this,
            };
            ability.SelectedSuccessfully += AbilityOnSelectedSuccessfully;
            ability.Start();
        };
        
        Controls.GetControlCursor().SetLine(Height - 1)
            .Print(_attackButton).Print(_defenceButton).Print(_abilitiesButton);
            
        
        _moveButton = new KeyedButton(string.Empty, Keys.Right)
        {
            ShowEnds = false,
        };
        _moveButton.Click += (_, __) => _player.Control.SetMove(Movement.Right);

        _playerPanel = new PlayerPanel(width: CELL_WIDTH - 1, height: CELL_HEIGHT - 1, _player);
        Children.Add(new PlayerFightView(Width - CELL_WIDTH * FightProcess.MAP_WIDTH, 
            CELL_HEIGHT * FightProcess.MAP_HEIGHT, 
            _player)
        {
            Position = new Point(CELL_WIDTH * FightProcess.MAP_WIDTH + 1, 0),
        });
        Children.Add(new FightLog(Width - 1, Height - CELL_HEIGHT * FightProcess.MAP_HEIGHT - 3)
        {
            Position = new Point(0, CELL_HEIGHT * FightProcess.MAP_HEIGHT + 1),
            
        });
        Children.Add(_playerPanel);
        foreach (var enemy in _fightProcess.Team2.Actors)
        {
            var panel = new EnemyPanel(width: CELL_WIDTH - 1, height: CELL_HEIGHT - 1, enemy);
            panel.Click += PanelOnClick;
            _enemyPanels.Add(enemy, panel);
            Children.Add(panel);
        }
        
        Update();
        IsDirty = true;
    }

    private void Team1OnDefeated(object? sender, EventArgs e)
    {
        new BackScreen<FailView>((_, _) => new MainMenu().Start()).Start();
    }

    private void Team2OnDefeated(object? sender, EventArgs e)
    {
        _player.LevelUp();
        GameState.Instance.StartScreen();
    }

    private void AbilityOnSelectedSuccessfully(IAbility ability)
    {
        this.Start();
        _selectedAbility = ability;
        if (ability.TargetIsRequired)
        {
            var items = _player.Team.AllWhere(ability.TargetKind);
            if (items.Count == 1)
                _player.Control.SetMove(new UseAbility(ability, items.First()));
            else if (items.Count > 1)
                StartSelectMode(items);
            else
            {
                Debug.Assert(false);
            }
        }
        else
            _player.Control.SetMove(new UseAbility(ability));
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
        _player.Control.SetMove(new UseAbility(_selectedAbility, panel.Actor));
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
        return _player.EnemiesOnAttackLine().SingleOrDefault();
    }

    public void Update()
    {
        this.Clear();

        DrawWalls();
        DrawPlayer();
        DrawEnemies();
    }

    private void DrawEnemies()
    {
        foreach (var enemy in _fightProcess.Team2.Actors)
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
        var (x, y) = _player.LocalPosition;
        var rect = new Rectangle(CELL_WIDTH * x + 1, CELL_HEIGHT * y + 1, CELL_WIDTH + 1, CELL_HEIGHT + 1);
        _playerPanel.Position = rect.Position;

        var movePosition = _player.LocalPosition + (1, 0);
        
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