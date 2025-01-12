using System.Diagnostics;
using PrimitiveAdventure.Core.Rpg.Controlling;

namespace PrimitiveAdventure.Core.Rpg;

public class FightProcess
{
    public const int MAP_WIDTH = 4;
    public const int MAP_HEIGHT = 3;
    
    private readonly Player _player;
    private readonly List<Actor> _enemies;
    private readonly List<Actor> _all;

    public IPlayer Player => _player;
    public IReadOnlyCollection<IActor> Enemies => _enemies.AsReadOnly();
    public IReadOnlyCollection<IActor> All => _all.AsReadOnly();

    public FightProcess(Player player, IEnumerable<Actor> enemies)
    {
        _player = player;
        _enemies = enemies.ToList();
        _all = _enemies.ToList();
        _all.Insert(0, _player);
        _player.Controller = new PlayerController(this);

        _player.LocalPosition = (0, 1);
        
        Debug.Assert(_enemies.Count > 0);
        Debug.Assert(_enemies.Count <= MAP_HEIGHT);
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].LocalPosition = (MAP_WIDTH - 1, i);
        }
    }

    private int index = 0;

    public void Run()
    {
        for (; ; index++)
        {
            var actor = _all[index % _all.Count];
            var controller = actor.Controller;
            if (controller.HasPredictedMove)
            {
                controller.Move.Apply(this, actor);
                controller.Update(this);
            }
            return;
        }
    }
}