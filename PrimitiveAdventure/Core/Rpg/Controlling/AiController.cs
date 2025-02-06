using PrimitiveAdventure.Core.Rpg.Fight;

namespace PrimitiveAdventure.Core.Rpg.Controlling;

public class AiController: IControllable
{
    private readonly IActor _actor;
    
    public AiController(IActor actor)
    {
        _actor = actor;
    }

    public IMove Move { get; private set; }

    public bool HasPredictedMove => true;
    
    public void Update(FightProcess fightProcess)
    {
        if (CanAttack(fightProcess))
            Move = new Attack(_target);
        else
            Move = new Movement(_actor.Team.Direction);
    }

    private IActor _target;
    
    private bool CanAttack(FightProcess fightProcess)
    {
        var enemies = _actor.EnemiesOnAttackLine();
        var result = enemies.Any();
        if (result && !enemies.Contains(_target))
            _target = enemies.First();
        return result;
    }
}