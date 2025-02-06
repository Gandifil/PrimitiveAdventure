using PrimitiveAdventure.Core.Rpg.Fight;

namespace PrimitiveAdventure.Core.Rpg.Controlling;

public class PlayerController: IControllable, IActorController
{
    public IMove Move { get; private set; }

    public bool HasPredictedMove => Move is not null;
    public void Update(FightProcess fightProcess)
    {
        Move = null;
    }
    
    private readonly FightProcess _fightProcess;

    public PlayerController(FightProcess fightProcess)
    {
        _fightProcess = fightProcess;
    }

    public void SetMove(IMove move)
    {
        Move = move;
        _fightProcess.Run();
    }
}