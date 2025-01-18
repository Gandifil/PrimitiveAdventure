namespace PrimitiveAdventure.Core.Rpg.Modifiers;

public record ParamMod(Parameters.Kind Kind, int Amount) : IModifierWithApply
{
    public string Line => @$"{Amount} : {Kind}";
    
    public void Apply(Actor actor)
    {
        actor.Parameters[Kind].Value += Amount;
    }
};