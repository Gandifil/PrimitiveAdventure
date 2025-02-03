namespace PrimitiveAdventure.Core.Rpg.Modifiers;

public record ParamMod(Parameters.Kind Kind, int Amount) : IModifier
{
    private static readonly Dictionary<Parameters.Kind, string> _templates = new()
    {
        {Parameters.Kind.AttackDamage, "+{0} к урону"},
        {Parameters.Kind.Armor, "+{0} к броне"},
        {Parameters.Kind.ArmorPenetration, "+{0} к пробитию брони"},
        {Parameters.Kind.ArmorDefenceBonus, "+{0} к бонусу брони при защите"},
        {Parameters.Kind.CriticalRate, "+{0}% к шансу крита"},
        {Parameters.Kind.CriticalDamage, "+{0}% к бонусу при крите"},
        {Parameters.Kind.CounterAttackRate, "+{0} к шансу контратаки"},
    };
    public string Line => string.Format(_templates[Kind], Amount);
    public void Assign(Actor actor)
    {
        actor.Parameters[Kind].Value += Amount;
    }

    public void Cancel(Actor actor)
    {
        actor.Parameters[Kind].Value -= Amount;
    }
};