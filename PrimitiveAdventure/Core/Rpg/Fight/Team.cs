using Coroutine;
using PrimitiveAdventure.Core.Rpg.Abilities;

namespace PrimitiveAdventure.Core.Rpg.Fight;

public interface ITeam
{
    IReadOnlyCollection<IActor> Actors { get; }
    IReadOnlyCollection<IActor> Enemies { get; }

    public IReadOnlyCollection<IActor> AllWhere(TargetKind targetKind)
    {
        switch (targetKind)
        {
            case TargetKind.Enemy: return Enemies;
            case TargetKind.Friend: return Actors;
            case TargetKind.Enemy | TargetKind.Friend: return Enemies.Union(Actors).ToList();
            default: return Array.Empty<IActor>();
        }
    }
    
    int Direction { get; }

    event EventHandler Defeated;
}

public class Team: ITeam
{
    private readonly List<Actor> _actors;
    private readonly List<Actor> _enemies;
    
    public required int Direction { get; init; }

    public IReadOnlyCollection<Actor> Actors => _actors;
    IReadOnlyCollection<IActor> ITeam.Actors => Actors;
    
    public IReadOnlyCollection<Actor> Enemies => _enemies;
    IReadOnlyCollection<IActor> ITeam.Enemies => _enemies;

    public event EventHandler Defeated;

    public Team(List<Actor> actorses, List<Actor> enemies)
    {
        _actors = actorses;
        _enemies = enemies;
    }

    public void Add(Actor actor)
    {
        var x = Direction == 1 ? 0 : FightProcess.MAP_WIDTH - 1;
        var y = _actors.Count switch
        {
            0 => 1,
            1 => 0,
            2 => 2,
        };
        actor.LocalPosition = (x, y);
        _actors.Add(actor);
        actor.Team = this;
    }

    public void CheckHealth()
    {
        var toRemove = _actors.Where(x => x.Health.Value <= 0);
        foreach (var actor in toRemove)
            Remove(actor);
    }

    private void Remove(Actor actor)
    {
        _actors.Remove(actor);
        if (_actors.Count == 0)
            Defeated?.Invoke(this, EventArgs.Empty);
    }
}