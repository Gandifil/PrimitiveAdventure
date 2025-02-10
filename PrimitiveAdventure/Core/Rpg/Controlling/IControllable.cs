using PrimitiveAdventure.Core.Rpg.Fight;

namespace PrimitiveAdventure.Core.Rpg.Controlling;

public interface IControllable
{
    IMove Move { get; }
    
    bool HasPredictedMove { get; }

    public event EventHandler Changed;
    void Update(FightProcess fightProcess);
}