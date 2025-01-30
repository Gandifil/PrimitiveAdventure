namespace PrimitiveAdventure.Core.Rpg.Masteries;

public abstract class Mastery: IMastery
{
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract ITalent[] Talents { get; }
    
    public override string ToString()
    {
        return Name.Prepare();
    }
}