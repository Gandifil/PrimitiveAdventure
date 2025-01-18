namespace PrimitiveAdventure.Core.Rpg.Modifiers;

public interface IModifierWithApply: IModifier
{
    void Apply(Actor actor);
}