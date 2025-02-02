namespace PrimitiveAdventure.Core.Rpg.Abilities;

public class SelfHeal: Ability
{
    public int Value => 5;
    
    public SelfHeal()
    {
        Name = "Self Heal";
        Description = $"восстанавливает {Value} здоровья";
        Cost = new CostData(Magic: 1);
    }

    protected override void Impact(Actor? target)
    {
        _owner.Health.Value += Value;
    }
}