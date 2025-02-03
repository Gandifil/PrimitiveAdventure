using System.Collections;

namespace PrimitiveAdventure.Core.Rpg.Masteries;

public class MasteryManager: IEnumerable<MasteryHandler>
{
    private readonly Player _player;
    private List<MasteryHandler> _masteryHandlers = new();

    public MasteryManager(Player player)
    {
        _player = player;
    }

    public int AllLevels => _masteryHandlers
        .SelectMany(m => m.TalentHandlers.Select(t => t.Level))
        .Sum();

    public IMastery[] GetFreeMastery() =>
        AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsAssignableTo(typeof(IMastery)) && !type.IsAbstract)
            .Select(type => Activator.CreateInstance(type) as IMastery)
            .ToArray();

    public void Add(IMastery mastery) => _masteryHandlers.Add(new MasteryHandler(mastery));
    
    private List<MasteryHandler>.Enumerator GetEnumerator() => _masteryHandlers.GetEnumerator();

    IEnumerator<MasteryHandler> IEnumerable<MasteryHandler>.GetEnumerator()
    {
        return _masteryHandlers.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _masteryHandlers.GetEnumerator();
    }
}