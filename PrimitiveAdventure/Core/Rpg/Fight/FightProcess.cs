using System.Diagnostics;
using PrimitiveAdventure.Core.Rpg.Controlling;

namespace PrimitiveAdventure.Core.Rpg.Fight;

public interface IFightProcess
{
    ITeam Team1 { get; }
    
    ITeam Team2 { get; }

    IEnumerable<IActor> All { get; }
}

public class FightProcess: IFightProcess
{
    public const int MAP_WIDTH = 4;
    public const int MAP_HEIGHT = 3;

    public ITeam Team1 => Teams[0]; //TODO: remove
    public ITeam Team2 => Teams[1]; //TODO: remove
    IEnumerable<IActor> IFightProcess.All => All;

    public IEnumerable<Actor> All => Teams[0].Actors.Union(Teams[1].Actors);
    
    public readonly Team[] Teams = new Team[2];

    public FightProcess()
    {
        var a = new List<Actor>();
        var b = new List<Actor>();
        Teams[0] = new Team(a, b)
        {
            Direction = 1,
        };
        Teams[1] = new Team(b, a)
        {
            Direction = -1,
        };
    }

    public FightProcess Start()
    {
        foreach (var team in Teams)
        {
            Debug.Assert(team.Actors.Count > 0);
            Debug.Assert(team.Actors.Count <= MAP_HEIGHT);
            team.Actors.ToList().ForEach(x => x.Controller.Update(this));
        }

        return this;
    }
    
    public void Run()
    {
        foreach (var actor in All.OrderBy(x => x.Controller.Move.Order))
            UseMove(actor, actor.Controller.Move);

        foreach (var actor in All)
        {
            actor.Tick();
            actor.Controller.Update(this);
        }
        
        foreach (var team in Teams)
            team.CheckHealth();
    }

    public readonly List<FightMoveLog> Moves = new ();

    private void UseMove(Actor actor, IMove move)
    {
        move.Apply(this, actor);
        Moves.Add(new FightMoveLog(actor, move));
    }
}