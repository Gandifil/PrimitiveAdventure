using PrimitiveAdventure.Core.Rpg.Masteries;

namespace PrimitiveAdventure.Masteries.HandToHandCombat;

public class HandToHandCombatMastery: Mastery
{
    public override string Name => "Рукопашный бой";
    public override string Description => @$"Стань неудержимой силой в сердце схватки. Ты — вихрь плоти и костей, танцующий в эпицентре хаоса. Твои кулаки обрушиваются, как молот, ломая доспехи; твои удары ногами рассекают воздух, сбивая врагов с ног. Здесь нет места страху — только ритм боя, зовущий тебя вплотную к противнику, где каждый выдох становится оружием, а каждая рана лишь подливает ярости в кровь.";

    public override ITalent[] Talents { get; } = {
        new AbilityTalent<VoidLoop>(),
        new SteelArmsTalent(),
        new SteelArmsTalent(),
        new SteelArmsTalent(),
    };
}