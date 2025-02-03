namespace PrimitiveAdventure.Core.Rpg.Modifiers;

public class ModifiersList: List<IModifier>
{
    public void Assign(Actor actor)
    {
        foreach (var mod in this)
            mod.Assign(actor);
    }

    public void Cancel(Actor actor)
    {
        foreach (var mod in this)
            mod.Cancel(actor);
    }
}