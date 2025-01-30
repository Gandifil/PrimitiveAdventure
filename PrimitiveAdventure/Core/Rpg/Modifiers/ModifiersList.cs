namespace PrimitiveAdventure.Core.Rpg.Modifiers;

public class ModifiersList: List<IModifier>
{
    void Assign(Actor actor)
    {
        foreach (var mod in this)
            mod.Assign(actor);
    }

    void Cancel(Actor actor)
    {
        foreach (var mod in this)
            mod.Cancel(actor);
    }
}