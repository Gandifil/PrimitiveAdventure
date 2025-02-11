using PrimitiveAdventure.Core.Rpg.Fight;

namespace PrimitiveAdventure.Core.Rpg.Controlling;

public class AiDefenceController: IControllable
{
    private readonly IActor _actor;
    
    public AiDefenceController(IActor actor)
    {
        _actor = actor;
    }

    public IMove Move { get; private set; }

    public event EventHandler Changed;

    public bool HasPredictedMove => true;
    
    public void Update(FightProcess fightProcess)
    {
        if (CanAttack(fightProcess))
        {
            _attackedLastTurn = !_attackedLastTurn;
            Move = _attackedLastTurn ? new Attack(_target) : new Defence();
        }
        else
            Move = new Movement(_actor.Team.Direction);
        Changed?.Invoke(this, EventArgs.Empty);
    }

    private IActor _target;
    private bool _attackedLastTurn;
    
    private bool CanAttack(FightProcess fightProcess)
    {
        var enemies = _actor.EnemiesOnAttackLine();
        var result = enemies.Any();
        if (result && !enemies.Contains(_target))
            _target = enemies.First();
        return result;
    }
}