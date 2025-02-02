using PrimitiveAdventure.Core.Rpg.Utils;

namespace PrimitiveAdventure.Core.Rpg.Effects;

public interface IEffect
{
    string Name { get; }
    
    IObservedValue<int> Power { get; }
    
    EffectKind Kind { get; }
}