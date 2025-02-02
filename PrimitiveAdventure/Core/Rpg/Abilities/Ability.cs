using System.Diagnostics;

namespace PrimitiveAdventure.Core.Rpg.Abilities;

public abstract class Ability: IAbility
{
    protected Actor _owner;
    
    public Ability()
    {
    }

    public void SetOwner(Actor newvalue)
    {
        _owner = newvalue;
    }

    public string Name { get; protected init; }
    
    public string Description { get; protected set; }
    public TargetKind TargetKind { get; protected init; }
    public bool TargetIsRequired => TargetKind != TargetKind.None;

    public CostData Cost { get; protected set;}
    
    public bool IsUsableBy(IActor p)
    {
        return _owner.Health.Value >= Cost.Health 
               && _owner.Magic.Value >= Cost.Magic
               && _owner.Stamina.Value >= Cost.Stamina;
    }

    public void Use(Actor? target)
    {
        Debug.Assert(IsUsableBy(_owner));
        Debug.Assert(target is not null == TargetIsRequired);
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