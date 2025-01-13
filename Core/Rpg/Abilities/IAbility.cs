using PrimitiveAdventure.Ui;

namespace PrimitiveAdventure.Core.Rpg.Abilities;

public interface IAbility: INamed
{
    string Description { get; }
    
    CostData Cost { get; }
    
    bool IsUsable(IPlayer p);

    void Use(Actor? target);
}