using PrimitiveAdventure.Core.Rpg.Controlling;
using PrimitiveAdventure.Core.Rpg.Utils;

namespace PrimitiveAdventure.Core.Rpg;

public interface IActor
{
    string Name { get; }
    
    Point LocalPosition { get; }
    
    ILimitedValue<int> Health { get; }
    
    IControllable Controller { get; }
    
    int Damage { get; }
    
    int Direction { get; }
    
    bool IsAlive { get; }
}

public abstract class Actor : IActor
{
    public string Name { get; protected set; }
    public Point LocalPosition { get; set; }

    public LimitedValue<int> Health { get; } = new(10);
    public IControllable Controller { get; set; }

    public int Damage { get; } = 1;
    public int Direction { get; set; } = -1;

    public bool IsAlive => Health.Value > 0;
    
    ILimitedValue<int> IActor.Health => Health;

    public void Attack(Actor target)
    {
        target.Health.Value -= Damage;
    }
}