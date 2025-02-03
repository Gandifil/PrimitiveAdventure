namespace PrimitiveAdventure.Core.Rpg.Masteries;

public record MasteryHandler(IMastery Mastery)
{
    public TalentHandler[] TalentHandlers { get; } = Mastery.Talents.Select(x => new TalentHandler(x)).ToArray();

    public TalentHandler? Get<T>() where T : ITalent
    {
        return TalentHandlers.FirstOrDefault(x => x.Talent is T);
    }
}