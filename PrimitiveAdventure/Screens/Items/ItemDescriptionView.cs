using PrimitiveAdventure.Core.Rpg.Items;

namespace PrimitiveAdventure.Screens.Items;

public class ItemDescriptionView: Console
{
    public ItemDescriptionView(int width, int height, IItem? item) : base(width, height)
    {
        if (item is null)
            return;

        Cursor.Print(item.Name.Prepare());
        if (item is IWeapon weapon)
            Cursor.NewLine().Print($"Урон: {weapon.Damage}".Prepare());
    }
}