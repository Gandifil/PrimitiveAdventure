using PrimitiveAdventure.Core.Rpg.Masteries;
using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.SadConsole.Effects;
using PrimitiveAdventure.Screens.Base;

namespace PrimitiveAdventure.Screens.Views;

public class TalentView: Console, IEntityView<TalentHandler>
{
    public TalentView(int width, int height) : base(width, height)
    {
    }

    public void Set(TalentHandler entity)
    {
        Surface.Clear();
        Cursor.Position = new Point(0, 0);
        Cursor.UseStringParser = true;
        
        Cursor.PrintText(entity.Talent.Description.Prepare());
        ViewHelper.Print(Cursor, entity.CurrentModifiersList);
        SadComponents.Add(new LineCharacterFade(TimeSpan.FromMilliseconds(500)));
    }
}