using PrimitiveAdventure.Core.Rpg.Modifiers;
using PrimitiveAdventure.SadConsole;
using SadConsole.Components;

namespace PrimitiveAdventure.Screens.Views;

public static class ViewHelper
{
    public static void Print(Cursor cursor, ModifiersList modifiersList)
    {
        foreach (var mod in modifiersList)
            cursor.NewLine().Tab().Print(mod.Line.Prepare());
    }
}