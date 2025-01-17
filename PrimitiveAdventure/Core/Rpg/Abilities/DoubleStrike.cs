namespace PrimitiveAdventure.Core.Rpg.Abilities;

public class DoubleStrike: Ability
{
    public DoubleStrike(Actor owner) : base(owner)
    {
        Name = "двойной удар";
        Description = $"наносит 5 урона цели";
        TargetKind = TargetKind.Enemy;
        Cost = new CostData(Stamina: 1);
    }

    protected override void Impact(Actor? target)
    {
        target!.Health.Value -= 5;
    }
}