namespace PrimitiveAdventure.Core.Rpg.Controlling;

public class AiController: IControllable
{
    private readonly Actor _actor;
    private readonly FightMapView _fightMap;
    
    public AiController(Actor actor, FightMapView fightMap)
    {
        _actor = actor;
        _fightMap = fightMap;
    }

    public IMove Move { get; private set; }

    public bool HasPredictedMove => true;
    
    public void Update(FightProcess fightProcess)
    {
        if (CanAttack(fightProcess))
            Move = new Attack(_target);
        else
            Move = new Movement(_actor.Direction);
    }

    private Actor _target;
    
    private bool CanAttack(FightProcess fightProcess)
    {
        var enemies = _fightMap.Enemies.Where(x => x.LocalPosition.X
                                                  == _actor.LocalPosition.X + _actor.Direction).ToList();
        var result = enemies.Any();
        if (result && !enemies.Contains(_target))
            _target = enemies.First();
        return result;
    }
}