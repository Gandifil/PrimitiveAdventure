using PrimitiveAdventure.Core.Rpg.Utils;

namespace PrimitiveAdventure.Core.Rpg;

public interface IParameters
{
    public IObservedValue<int> this[Parameters.Kind index] { get; }
}

public class Parameters: IParameters
{
    public enum Kind
    {
        AttackDamage,
        Test1,
    }

    private readonly Dictionary<Kind, ObservedValue<int>> _values = new();

    public Parameters()
    {
        foreach (Kind kind in Enum.GetValues(typeof(Kind)))
            _values[kind] = new ObservedValue<int>(0);
    }

    IObservedValue<int> IParameters.this[Kind index] => _values[index];

    public ObservedValue<int> this[Kind index] => _values[index];
}