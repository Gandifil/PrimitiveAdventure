namespace PrimitiveAdventure.Core.Rpg.Modifiers;

public record ParamMod(Parameters.Kind Kind, int Amount) : IModifier
{
    public string Line => @$"{Amount} : {Kind}";
    public void Assign(Actor actor)
    {
        actor.Parameters[Kind].Value += Amount;
    }

    public void Cancel(Actor actor)
    {
        actor.Parameters[Kind].Value -= Amount;
    }
};