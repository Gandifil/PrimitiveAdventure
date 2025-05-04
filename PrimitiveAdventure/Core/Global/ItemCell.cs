using PrimitiveAdventure.Core.Rpg.Items;
using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.Screens;
using PrimitiveAdventure.Screens.Items;

namespace PrimitiveAdventure.Core.Global;

public class ItemCell(IItem Item): IGlobalMapCell
{
    public IScreenObject View
    {
        get
        {
            var console = new Console(MapScreen.CELL_WIDTH, MapScreen.CELL_HEIGHT);
            console.Cursor.Print("f");
            return console;
        }
    }

    public void OnCollisionWith(Player player)
    {
        new ChooseItemScreen(player, Item)
        {
            BackScreen = Game.Instance.Screen,
        }.Start();
    }
}