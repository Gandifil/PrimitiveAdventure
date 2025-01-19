using PrimitiveAdventure.Core.Rpg.Items;
using PrimitiveAdventure.SadConsole.Screens;

namespace PrimitiveAdventure.Screens.Items;

public class ItemView: BaseScreen
{
    public ItemView(int width, int height, IItem? item) : base(width, height)
    {
        Children.Add(new ItemDescriptionView(Width, Height, item));
    }
}