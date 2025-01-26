namespace PrimitiveAdventure.Core.Rpg.Modifiers;

public interface IHasModifiers
{
    IReadOnlyList<IModifier> Modifiers { get; }
}