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

    bool IsDefenced { get; }

    event Action Attacked;
}