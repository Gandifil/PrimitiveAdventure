using PrimitiveAdventure.Ui;

namespace PrimitiveAdventure.Core.Rpg.Abilities;

public interface IAbility: INamed
{
    string Description { get; }
    
    TargetKind TargetKind { get; }
    
    bool TargetIsRequired { get; }
    
    CostData Cost { get; }
    
    bool IsUsableBy(IActor? p = null);
}