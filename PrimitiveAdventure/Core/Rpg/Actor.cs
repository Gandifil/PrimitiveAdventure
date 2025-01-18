using PrimitiveAdventure.Core.Rpg.Abilities;
using PrimitiveAdventure.Core.Rpg.Controlling;
using PrimitiveAdventure.Core.Rpg.Utils;

namespace PrimitiveAdventure.Core.Rpg;

public interface IActor
{
    string Name { get; }
    
    Point LocalPosition { get; }
    
    ILimitedValue<int> Health { get; }
    
    ILimitedValue<int> Magic { get; }
    
    ILimitedValue<int> Stamina { get; }
    
    IReadOnlyList<IAbility> Abilities { get; }
    
    IParameters Parameters { get; }
    
    IControllable Controller { get; }
    
    int Damage { get; }
    
    int Direction { get; }
    
    bool IsAlive { get; }

    event Action Attacked;
}

public abstract class Actor : IActor
{
    public string Name { get; protected set; }
    public Point LocalPosition { get; set; }

    public LimitedValue<int> Health { get; } = new(10);

    public LimitedValue<int> Magic { get; } = new(10);

    public LimitedValue<int> Stamina { get; } = new(10);

    public List<Ability> Abilities { get; } = new ();
    
    public Parameters Parameters { get; } = new();

    IParameters IActor.Parameters => Parameters;
    
    IReadOnlyList<IAbility> IActor.Abilities => Abilities.AsReadOnly();
    
    public IControllable Controller { get; set; }

    public virtual int Damage { get; } = 1;
    public int Direction { get; set; } = -1;

    public bool IsAlive => Health.Value > 0;
    
    ILimitedValue<int> IActor.Health => Health;
    
    ILimitedValue<int> IActor.Magic => Magic;
    
    ILimitedValue<int> IActor.Stamina => Stamina;

    public void Attack(Actor target)
    {
        target.Health.Value -= Damage;
        target.OnAttacked();
    }

    public event Action Attacked;

    private void OnAttacked()
    {
        Attacked?.Invoke();
    }
}