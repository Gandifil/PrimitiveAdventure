using PrimitiveAdventure.Core.Rpg.Controlling;

namespace PrimitiveAdventure.Core.Rpg.Fight;

public class FightBuilder
{
    private readonly FightProcess _fightProcess = new FightProcess();

    public void AddPlayer(Player player)
    {
        player.Controller = new PlayerController(_fightProcess);
        _fightProcess.Teams[0].Add(player);
    }

    public void Add(Actor actor)
    {
        _fightProcess.Teams[1].Add(actor);
    }

    public void Add(IEnumerable<Actor> actors)
    {
        foreach (var actor in actors)
            Add(actor);
    }

    public FightProcess Build()
    {
        _fightProcess.Start();
        return _fightProcess;
    }
}