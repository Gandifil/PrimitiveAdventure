using PrimitiveAdventure.Core.Rpg.Items;
using PrimitiveAdventure.Core.Rpg.Modifiers;
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
        Armor,
        ArmorPenetration,
        CriticalRate,
        CriticalDamage,
        CounterAttackRate,
    }

    private readonly Dictionary<Kind, ObservedValue<int>> _values = new();

    public Parameters()
    {
        foreach (Kind kind in Enum.GetValues(typeof(Kind)))
            _values[kind] = new ObservedValue<int>(0);
        Reset();
    }

    IObservedValue<int> IParameters.this[Kind index] => _values[index];

    public ObservedValue<int> this[Kind index] => _values[index];

    public void Reset()
    {
        foreach (var (kind, _) in _values)
            _values[kind].Value = kind switch
            {
                Kind.CriticalRate => 5,
                Kind.CriticalDamage => 200,
                _ => 0,
            };
    }
}