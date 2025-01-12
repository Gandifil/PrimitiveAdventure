namespace PrimitiveAdventure.Core.Rpg.Controlling;

public class AiController: IControllable
{
    private readonly Actor _actor;
    
    public AiController(Actor actor)
    {
        _actor = actor;
    }

    public IMove Move { get; private set; }

    public bool HasPredictedMove => true;
    public void Update(FightProcess fightProcess)
    {
        Move = new Movement(true);
    }
}