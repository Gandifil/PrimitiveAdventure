using PrimitiveAdventure.Core.Rpg.Effects;

namespace PrimitiveAdventure.Masteries.HandToHandCombat;

public class ComboEffect: Effect
{
    public override string Name => "Комбо";
    
    public override EffectKind Kind => EffectKind.Friend;
}