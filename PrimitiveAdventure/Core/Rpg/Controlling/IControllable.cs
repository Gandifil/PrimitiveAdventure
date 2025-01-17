namespace PrimitiveAdventure.Core.Rpg.Controlling;

public interface IControllable
{
    IMove Move { get; }
    
    bool HasPredictedMove { get; }
    void Update(FightProcess fightProcess);
}