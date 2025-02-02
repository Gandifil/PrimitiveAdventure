using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Abilities;

namespace PrimitiveAdventure.Masteries.HandToHandCombat;

public abstract class ComboAbility: Ability
{
    public ComboAbility()
    {
        Description += "\nЕсли предыдущее действие не совпадает с текущим, дает Комбо, если совпадает - забирает.";
        Description += "\nКаждое комбо увеличивает урон на 1.";
        TargetKind = TargetKind.Enemy;
    }

    protected override void Impact(Actor? target)
    {
        // TODO: check last action
        _owner.Effects.Add(new ComboEffect());
    }
}