namespace PrimitiveAdventure.Core.Rpg.Masteries;

public interface IMastery
{
    string Name { get; }
    
    string Description { get; }
    
    ITalent[] Talents { get; }
}