using PrimitiveAdventure.Screens;
using PrimitiveAdventure.Screens.Views;

namespace PrimitiveAdventure.Core.Global;

public class PlayerCell(Player player): IGlobalMapCell
{
    public Player Player = player;

    public Point Position { get; set; }

    public IScreenObject View => new ResourceView(MapScreen.CELL_WIDTH, MapScreen.CELL_HEIGHT, Player.RESOURCE_NAME);
}