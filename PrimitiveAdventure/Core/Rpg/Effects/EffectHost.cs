using System.Collections;

namespace PrimitiveAdventure.Core.Rpg.Effects;

public interface IEffectHost : IEnumerable<IEffect>
{
}

public class EffectHost: IEffectHost, IEnumerable<Effect>
{
    private readonly List<Effect> _effects = new();
    private readonly Actor _actor;

    public EffectHost(Actor actor)
    {
        _actor = actor;
    }

    public TEffect? Get<TEffect>() where TEffect : class, IEffect
    {
        foreach (var effect in _effects)
            if (effect is TEffect foundEffect)
                return foundEffect;
        return null;
    }

    public bool Has<TEffect>() where TEffect : class, IEffect
    {
        return Get<TEffect>() != null;
    }
    
    public void Tick()
    {
        _effects.ForEach(effect => effect.Tick(_actor));
    }

    public void Add(Effect effect)
    {
        _effects.Add(effect);
        effect.Power.Changed += (value, _) => { if (value <= 0) Remove(effect); };
        effect.Added(_actor);
    }

    public void Remove(Effect effect)
    {
        _effects.Remove(effect);
        effect.Removed(_actor);
    }

    IEnumerator<Effect> IEnumerable<Effect>.GetEnumerator()
    {
        return _effects.GetEnumerator();
    }

    IEnumerator<IEffect> IEnumerable<IEffect>.GetEnumerator()
    {
        return _effects.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _effects.GetEnumerator();
    }
}