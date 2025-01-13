namespace PrimitiveAdventure.Core.Rpg.Abilities;

public abstract class Ability: IAbility
{
    protected readonly Actor _owner;
    
    public Ability(Actor owner)
    {
        _owner = owner;
    }

    public string Name { get; protected set; }
    
    public string Description { get; protected set; }
    
    public CostData Cost { get; protected set;}
    
    public bool IsUsable(IPlayer p)
    {
        return _owner.Health.Value > Cost.Health 
               && _owner.Magic.Value > Cost.Magic
               && _owner.Stamina.Value > Cost.Stamina;
    }

    public void Use(Actor? target)
    {
        Pay();
        Impact(target);
    }

    protected abstract void Impact(Actor? target);

    private void Pay()
    {
        _owner.Health.Value -= Cost.Health;
        _owner.Magic.Value -= Cost.Magic;
        _owner.Stamina.Value -= Cost.Stamina;
    }

    public override string ToString()
    {
        return Name.Prepare();
    }
}