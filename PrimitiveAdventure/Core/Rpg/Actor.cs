using PrimitiveAdventure.Core.Rpg.Abilities;
using PrimitiveAdventure.Core.Rpg.Controlling;
using PrimitiveAdventure.Core.Rpg.Effects;
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
    public EffectHost Effects { get; private set; }
    IEffectHost IActor.Effects => Effects;

    IParameters IActor.Parameters => Parameters;
    
    IReadOnlyList<IAbility> IActor.Abilities => Abilities.AsReadOnly();
    
    public IControllable Controller { get; set; }

    public virtual int Damage { get; } = 1;
    public int Direction { get; set; } = -1;

    public bool IsAlive => Health.Value > 0;
    
    ILimitedValue<int> IActor.Health => Health;
    
    ILimitedValue<int> IActor.Magic => Magic;
    
    ILimitedValue<int> IActor.Stamina => Stamina;
    public bool IsDefenced { get; set; }

    public Actor()
    {
        Effects = new EffectHost(this);
    }

    public void Attack(Actor target)
    {
        var isCritical = Dice(Parameters[Parameters.Kind.CriticalRate]);

        var damageRate = isCritical ? Parameters[Parameters.Kind.CriticalDamage] : 100;
        var damage = Damage * damageRate / 100;
        var armor = target.Parameters[Parameters.Kind.Armor] + 
                    (target.IsDefenced? target.Parameters[Parameters.Kind.ArmorDefenceBonus] : 0);
        damage = Math.Max(0, damage - armor);
        target.Health.Value -= damage;
        target.OnAttacked(this);
        
        FightLog.Instance.PrintLine(GetAttackText(target, isCritical, damage));
        
        // TODO: potential dead lock
        if (Dice(Parameters[Parameters.Kind.RepeatAttackRate]))
            Attack(target);
    }

    private string GetAttackText(Actor target, bool isCritical, int damage)
    {
        var templates = new List<FightLogTemplate>
        {
            // Обычные удары (не критические, не заблокированные)
            new FightLogTemplate(false, false, "{0} врезается оружием в {1} — {2} урона!"),
            new FightLogTemplate(false, false, "{0} бьёт {1} в плечо — сталь скользит по доспехам! ({2})"),
            new FightLogTemplate(false, false, "Резкий взмах! {1} отшатывается от удара. ({2})"),
            new FightLogTemplate(false, false, "{0} пронзает {1} — кровь сочится по лезвию. ({2})"),
            new FightLogTemplate(false, false, "Глухой удар! {1} теряет равновесие. ({2})"),

            // Критические удары (критические, не заблокированные)
            new FightLogTemplate(true, false, "{0} вонзает оружие в *самое сердце* брони {1} — **сокрушительный** {2}!"),
            new FightLogTemplate(true, false, "***СТИХИЯ БИТВЫ!*** {0} крушит {1} — кости трещат, доспехи **раскалываются**! ({2})"),
            new FightLogTemplate(true, false, "***СМЕРТОНОСНЫЙ ВИХРЬ!*** Удар {0} сносит {1} с ног — **критическая рана**! ({2})"),
            new FightLogTemplate(true, false, "{0} *рассекает* воздух — {1} не успевает крикнуть! **Молния в плоти**! ({2})"),
            new FightLogTemplate(true, false, "***СМЕРТЬ ШЁПОТОМ...*** {0} ловит слабину в защите — лезвие **впивается** в горло {1}! ({2})"),

            // Блокировки броней (не критические, заблокированные)
            new FightLogTemplate(false, true, "{0} бьёт {3} по щиту {1} — **броня непоколебима**! (0 урона)"),
            new FightLogTemplate(false, true, "Удар {0} отскакивает от доспехов {1} — лишь искры летят в ответ. (0)"),
            new FightLogTemplate(false, true, "{1} ловко парирует {3} — сталь **звенит**, но не сдаётся. (0)"),
            new FightLogTemplate(false, true, "Глухой лязг! {3} {0} бессильно скользит по пластинам {1}. (0)"),
            new FightLogTemplate(false, true, "{0} атакует — {1} даже не шелохнулся. **Стена из металла**! (0)"),

            // Критические блокировки (критические, заблокированные)
            new FightLogTemplate(true, true, "***СОКРУШИТЕЛЬНЫЙ УДАР...*** Но броня {1} **поглощает** всю ярость {0}! (0)"),
            new FightLogTemplate(true, true, "***ВИХРЬ БЕЗ СИЛЫ!*** {0} вкладывает всю мощь в удар — доспехи {1} **непреклонны**. (0)"),
            new FightLogTemplate(true, true, "{0} бьёт {3} с рёвом — {1} **смеётся**, стирая пыль с наплечников. (0)"),
            new FightLogTemplate(true, true, "***НЕПРОБИВАЕМО!*** Даже критическая сила {0} разбивается о щит {1}. (0)"),
            new FightLogTemplate(true, true, "***ХОЛОД БЕЗУЧАСТНОСТИ...*** Атака {0} замирает, не достигнув кожи {1}. (0)")
        };
        
        var correctTemplates = templates
            .Where(x => x.IsCritical == isCritical && x.IsBlocked == (damage == 0)).ToArray();
        
        return string.Format(Random.Shared.GetItems(correctTemplates, 1).First().Template, 
            ColoredStringBuilder.Name(Name), 
            ColoredStringBuilder.Name(target.Name), 
            ColoredStringBuilder.Damage("-" + damage.ToString()),
            "рукой");
    }

    private bool Dice(int rate)
    {
        if (rate > 0)
            return Random.Shared.Next(0, 100) < rate;
        
        return false;
    }

    public event Action Attacked;

    private void OnAttacked(Actor attacker)
    {
        if (IsDefenced && Dice(Parameters[Parameters.Kind.CounterAttackRate]))
            Attack(attacker);
        Attacked?.Invoke();
    }

    public void Tick()
    {
        IsDefenced = false;
    }
    
    public FightMapView MapView { get; set; }

    public IActor[] EnemiesOnAttackLine()
    {
        return MapView.Enemies.Where(x => x.LocalPosition.X == LocalPosition.X + Direction)
            .ToArray<IActor>();
    }

    public void Assign(FightMapView mapView)
    {
        MapView = mapView;
    }
}