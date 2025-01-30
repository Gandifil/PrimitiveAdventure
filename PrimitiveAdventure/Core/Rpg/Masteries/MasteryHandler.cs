namespace PrimitiveAdventure.Core.Rpg.Masteries;

class MasteryHandler(IMastery Mastery)
{
    public TalentHandler[] TalentHandlers { get; } = Mastery.Talents.Select(x => new TalentHandler(x)).ToArray();
    
}