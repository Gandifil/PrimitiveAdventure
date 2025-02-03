using PrimitiveAdventure.Core.Rpg.Masteries;
using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.SadConsole.Effects;
using PrimitiveAdventure.Screens.Base;
using SadConsole.UI;

namespace PrimitiveAdventure.Screens.Views;

public class MasteryView: ControlsConsole, IEntityView<IMastery>
{
    public MasteryView(int width, int height) : base(width, height)
    {
    }

    public void Set(IMastery entity)
    {
        Surface.Clear();
        Cursor.Position = new Point(0, 0);
        
        Cursor.PrintText(entity.Description.Prepare());
        SadComponents.Add(new LineCharacterFade(TimeSpan.FromMilliseconds(500)));
    }
}