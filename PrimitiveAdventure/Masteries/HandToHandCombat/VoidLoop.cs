using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Screens.Fight;

namespace PrimitiveAdventure.Masteries.HandToHandCombat;

public class VoidLoop: ComboAbility
{
    private static readonly FightLogTemplate _template = new FightLogTemplate(null, null,
        "Магия взорвалась внутри {1} ({2})! {0} переполняет сила ({4})!");
    
    public VoidLoop()
    {
        Name = "Петля Пустоты";
        Description = "[c:r f:Yellow]Магия - лишь иллюзия. Иллюзии разрываются[c:u 1]\nЗабирает треть магии цели (минимум - 5), восполняет за счет нее запас сил и наносит урон." 
                      + Description;
        Cost = new CostData(Magic: 10, Stamina: 10);
    }

    protected override void Impact(Actor? target)
    {
        base.Impact(target);
        
        var magic = Math.Max(5, target!.Magic.Value / 3);

        if (target!.Magic.Value < magic)
            magic = target!.Magic.Value;
        
        target!.Health.Value -= magic;
        target!.Magic.Value -= magic;
        _owner.Stamina.Value += magic;
        
        FightLog.Instance.PrintLine(_template.Build(_owner.Name, target.Name, magic, magic));
    }
}