using System.Diagnostics;

namespace PrimitiveAdventure.Core.Rpg;

public class FightProcess
{
    public const int MAP_WIDTH = 4;
    public const int MAP_HEIGHT = 3;
    
    private readonly Player _player;
    private readonly List<Actor> _enemies;

    public IPlayer Player => _player;
    public IReadOnlyCollection<IActor> Enemies => _enemies.AsReadOnly();

    public FightProcess(Player player, IEnumerable<Actor> enemies)
    {
        _player = player;
        _enemies = enemies.ToList();

        _player.LocalPosition = (0, 1);
        
        Debug.Assert(_enemies.Count > 0);
        Debug.Assert(_enemies.Count <= MAP_HEIGHT);
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].LocalPosition = (MAP_WIDTH - 1, i);
        }
    }


}