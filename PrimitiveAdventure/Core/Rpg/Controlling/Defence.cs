using PrimitiveAdventure.Core.Rpg.Fight;
using PrimitiveAdventure.Screens.Fight;

namespace PrimitiveAdventure.Core.Rpg.Controlling;

public class Defence: IMove
{
    public int Order => 1;
    
    public string DisplayText { get; }
    
    public void Apply(FightProcess fightProcess, Actor actor)
    {
        actor.IsDefenced = true;
        var defenseTemplates = new List<FightLogTemplate>
        {
            // Подготовка к защите (активные действия перед атакой)
            new FightLogTemplate(false, false, "{0} прикрывается {3}, готовясь к натиску!"),
            new FightLogTemplate(false, false, "{0} встаёт в боевую стойку — **взгляд словно сталь**."),
            new FightLogTemplate(false, false, "Руны на щите {0} вспыхивают — **магический барьер активирован**!"),
            new FightLogTemplate(false, false, "{0} отступает к стене, **используя окружение как укрытие**."),
            new FightLogTemplate(false, false, "Искры! {0} заряжает кинетический щит — **защита +{2}**."),
        };
        
        FightLog.Instance.PrintLine(string.Format(Random.Shared.GetItems(defenseTemplates.ToArray(), 1).First().Template, 
            actor.Name, null, actor.Parameters[Parameters.Kind.ArmorDefenceBonus], "щитом"));
    }
}