using PrimitiveAdventure.Core.Rpg.Masteries;

namespace PrimitiveAdventure.Masteries.WeaponsExpertise;

public class WeaponsExpertiseMastery: Mastery
{
    public override string Name => "Мастер оружия";
    public override string Description => @$"Каждый клинок — это язык, на котором говорит ваша воля к победе.";

    public override ITalent[] Talents { get; } =
    [
        new CounterattackSkillTalent(),
        new CounterattackSkillTalent(),
        new DaggerTalent(),
        new DaggerTalent(),
        new MaceTalent(),
        new MaceTalent(),
    ];
}