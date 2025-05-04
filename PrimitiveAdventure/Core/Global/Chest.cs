using PrimitiveAdventure.Screens;
using PrimitiveAdventure.Screens.Base;

namespace PrimitiveAdventure.Core.Global;

public class Chest: IGlobalMapCell
{
    public string RenderName => "сундук";

    public IScreenObject View
    {
        get
        {
            var console = new Console(MapScreen.CELL_WIDTH, MapScreen.CELL_HEIGHT);
            console.Cursor.Print(RenderName.Prepare());
            return console;
        }
    }
}