using PrimitiveAdventure.Core.Rpg.Abilities;
using PrimitiveAdventure.Core.Rpg.Controlling;
using PrimitiveAdventure.Core.Rpg.Utils;
using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.Screens.Fight;

namespace PrimitiveAdventure.Core.Rpg;

public abstract class Actor : IActor
{
    public string Name { get; protected set; }
    public Point LocalPosition { get; set; }

    public LimitedValue<int> Health { get; } = new(10);

    public LimitedValue<int> Magic { get; } = new(10);

    public LimitedValue<int> Stamina { get; } = new(10);

    public List<Ability> Abilities { get; } = new ();
    
    public Parameters Parameters { get; } = new();

    IParameters IActor.Parameters => Parameters;
    
    IReadOnlyList<IAbility> IActor.Abilities => Abilities.AsReadOnly();
    
    public IControllable Controller { get; set; }

    public virtual int Damage { get; } = 1;
    public int Direction { get; set; } = -1;

    public bool IsAlive => Health.Value > 0;
    
    ILimitedValue<int> IActor.Health => Health;
    
    ILimitedValue<int> IActor.Magic => Magic;
    
    ILimitedValue<int> IActor.Stamina => Stamina;

    public void Attack(Actor target)
    {
        var isCritical = Dice(Parameters[Parameters.Kind.CriticalRate]);

        var damageRate = isCritical ? Parameters[Parameters.Kind.CriticalDamage] : 100;
        var damage = Damage * damageRate / 100;
        target.Health.Value -= damage;
        target.OnAttacked();
        
        FightLog.Instance.PrintLine(GetAttackText(target, isCritical, damage));
    }

    private string GetAttackText(Actor target, bool isCritical, int damage)
    {
        string[] templates =
        [
            "{0} врезается оружием в {1} — {2} урона!",
            "{0} совершает резкий взмах! {1} отшатывается от удара. ({2})"
        ];
        
        return string.Format(Random.Shared.GetItems(templates, 1).First(), 
            ColoredStringBuilder.Name(Name), 
            ColoredStringBuilder.Name(target.Name), 
            ColoredStringBuilder.Damage("-" + damage.ToString()));
    }

    private bool Dice(int rate)
    {
        if (rate > 0)
            return Random.Shared.Next(0, 100) < rate;
        
        return false;
    }

    public event Action Attacked;

    private void OnAttacked()
    {
        Attacked?.Invoke();
    }
}