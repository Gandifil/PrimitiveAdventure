namespace PrimitiveAdventure.Core.Rpg.Modifiers;

public interface IModifier
{
    string Line { get; }
    
    void Assign(Actor actor);
    
    void Cancel(Actor actor);
}