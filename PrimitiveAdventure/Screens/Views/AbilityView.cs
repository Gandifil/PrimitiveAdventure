using PrimitiveAdventure.Core.Rpg.Abilities;
using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.SadConsole.Effects;
using PrimitiveAdventure.Screens.Base;

namespace PrimitiveAdventure.Screens.Views;

public class AbilityView: Console, IEntityView<IAbility>
{
    public AbilityView(int width, int height) : base(width, height)
    {
        Cursor.PrintAppearanceMatchesHost = false;
    }

    public void Set(IAbility entity)
    {
        //SadComponents.Clear();
        Surface.Clear();
        Cursor.Position = Point.Zero;
        Cursor.UseStringParser = true;
        

        var Selected = entity;
        var cursor = Cursor;
        cursor.PrintText(Selected.Description.Prepare());

        if (Selected.TargetIsRequired)
        {
            cursor.NewLine().Tab().Print("Требуется цель:".Prepare());
            if (Selected.TargetKind.HasFlag(TargetKind.Friend))
                cursor.NewLine().Tab().Tab().SetPrintAppearance(Color.Green).Print("Друг".Prepare());
            if (Selected.TargetKind.HasFlag(TargetKind.Enemy))
                cursor.NewLine().Tab().Tab().SetPrintAppearance(Color.Red).Print("Враг".Prepare());
            cursor.SetPrintAppearanceToHost();
        }

        if (Selected.Cost.Health != 0 || Selected.Cost.Magic != 0 || Selected.Cost.Stamina != 0)
        {
            cursor.NewLine().Print("цена:".Prepare());
            if (Selected.Cost.Health != 0)
                cursor.NewLine().Tab()
                    .Print("жизнь: ".Prepare()).Print(Selected.Cost.Health.ToString());
            if (Selected.Cost.Magic != 0)
                cursor.NewLine().Tab()
                    .Print("магия: ".Prepare()).Print(Selected.Cost.Magic.ToString());
            if (Selected.Cost.Stamina != 0)
                cursor.NewLine().Tab()
                    .Print("запас сил: ".Prepare()).Print(Selected.Cost.Stamina.ToString());
        }
        
        SadComponents.Add(new LineCharacterFade(TimeSpan.FromMilliseconds(500)));
    }
}