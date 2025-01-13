namespace PrimitiveAdventure.Core.Rpg.Abilities;

public interface IAbility
{
    string Name { get; }
    
    CostData Cost { get; }
    
    string Description { get; }
    
    void Apply(Actor p);
}