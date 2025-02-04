using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Abilities;

namespace PrimitiveAdventure.Masteries.WeaponsExpertise;

public class StrikesWhirlwindAbility: Ability
{
    public StrikesWhirlwindAbility()
    {
        Name = "Вихрь ударов";
        Description = "[c:r f:Yellow]Лезвие - продолжение души[c:u 1]\nГерой трижды атакует противника за один ход." 
                      + Description;
        Cost = new CostData(Stamina: 5);
        TargetKind = TargetKind.Enemy;
    }
    
    protected override void Impact(Actor? target)
    {
        _owner.Attack(target!);
        _owner.Attack(target!);
        _owner.Attack(target!);
    }
}