using PrimitiveAdventure.Core.Rpg.Items;
using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.Screens.Items;

namespace PrimitiveAdventure.Core.Global;

public class ItemCell(IItem Item): IGlobalMapCell
{
    public string Name { get; } = "f";
    public string? Resource { get; } = null;

    public void OnCollisionWith(Player player)
    {
        new ChooseItemScreen(player, Item).Start();
    }
}