namespace PrimitiveAdventure.Core.Rpg.Controlling;

public class Defence: IMove
{
    public int Order => 1;
    
    public string DisplayText { get; }
    
    public void Apply(FightProcess fightProcess, Actor actor)
    {
        actor.IsDefenced = true;
    }
}