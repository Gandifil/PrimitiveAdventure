using PrimitiveAdventure.Core.Rpg.Utils;

namespace PrimitiveAdventure.Core.Rpg.Effects;

public abstract class Effect: IEffect
{
    public virtual void Added(Actor actor) { }

    public virtual void Tick(Actor actor){}
    
    public virtual void Removed(Actor actor) { }
    public abstract string Name { get; }
    public abstract EffectKind Kind { get; }
    public ObservedValue<int> Power { get; } = new (1);
    IObservedValue<int> IEffect.Power => Power;
}