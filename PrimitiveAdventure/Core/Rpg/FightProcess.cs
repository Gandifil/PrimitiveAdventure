using System.Diagnostics;
using PrimitiveAdventure.Core.Rpg.Controlling;

namespace PrimitiveAdventure.Core.Rpg;

public class FightProcess
{
    public const int MAP_WIDTH = 4;
    public const int MAP_HEIGHT = 3;
    
    private readonly Player _player;
    private readonly List<Actor> _team1;
    private readonly List<Actor> _team2;
    private readonly List<Actor> _all;

    private readonly FightMapView _fightMapViewTeam1;
    private readonly FightMapView _fightMapViewTeam2;

    public IPlayer Player => _player;
    public IReadOnlyCollection<Actor> Team1 => _team1.AsReadOnly();
    public IReadOnlyCollection<Actor> Team2 => _team2.AsReadOnly();
    public IReadOnlyCollection<Actor> All => _all.AsReadOnly();
    public IFightMapView MapTeam1 => _fightMapViewTeam1;
    
    public IFightMapView MapTeam2 => _fightMapViewTeam2;

    public FightProcess(Player player, IEnumerable<Actor> enemies)
    {
        _player = player;
        _team1 = new List<Actor> { _player };
        _team2 = enemies.ToList();
        _all = _team1.Union(_team2).ToList();
        
        _player.Controller = new PlayerController(this);
        _player.LocalPosition = (0, 1);
        
        Debug.Assert(Team2.Count > 0);
        Debug.Assert(Team2.Count <= MAP_HEIGHT);

        _fightMapViewTeam1 = new FightMapView(this, false);
        _fightMapViewTeam2 = new FightMapView(this, true);
        for (int i = 0; i < _team1.Count; i++)
        {
            _team1[i].Assign(_fightMapViewTeam1);
        }
        for (int i = 0; i < _team2.Count; i++)
        {
            _team2[i].Controller = new AiController(_team2[i], _fightMapViewTeam2);
            _team2[i].Assign(_fightMapViewTeam2);
            _team2[i].LocalPosition = (MAP_WIDTH - 1, i);
        }
        _all.ForEach(x => x.Controller.Update(this));
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void Run()
    {
        foreach (var actor in _all.OrderBy(x => x.Controller.Move.Order))
            actor.Controller.Move.Apply(this, actor);

        foreach (var actor in _all)
        {
            actor.Tick();
            actor.Controller.Update(this);
        }
    }
}