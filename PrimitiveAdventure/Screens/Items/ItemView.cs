using PrimitiveAdventure.Core.Rpg.Items;
using PrimitiveAdventure.SadConsole.Screens;
using PrimitiveAdventure.Screens.Base;

namespace PrimitiveAdventure.Screens.Items;

public class ItemView: BaseScreen
{
    public ItemView(int width, int height, IItem? item) : base(width, height)
    {
        Children.Add(new PlaysciiView(Width, Height / 2, item.ResourceName));
        Children.Add(new ItemDescriptionView(Width, Height / 2, item)
        {
            Position = (0, Height / 2),
        });
    }
}