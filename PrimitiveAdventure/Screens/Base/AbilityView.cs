using PrimitiveAdventure.Core.Rpg.Abilities;
using PrimitiveAdventure.SadConsole.Effects;
using PrimitiveAdventure.Ui;

namespace PrimitiveAdventure.Screens.Base;

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

        var Selected = entity;
        var cursor = Cursor;
        cursor.Position = new Point(0, 0);
        cursor.NewLine().Print(Selected.Description.Prepare());

        if (Selected.TargetIsRequired)
        {
            cursor.NewLine().Print("Требуется цель:".Prepare());
            if (Selected.TargetKind.HasFlag(TargetKind.Friend))
                cursor.NewLine().SetPrintAppearance(Color.Green).Print("Друг".Prepare());
            if (Selected.TargetKind.HasFlag(TargetKind.Enemy))
                cursor.NewLine().SetPrintAppearance(Color.Red).Print("Враг".Prepare());
            cursor.SetPrintAppearanceToHost();
        }

        if (Selected.Cost.Health != 0 || Selected.Cost.Magic != 0 || Selected.Cost.Stamina != 0)
        {
            cursor.NewLine().Print("цена:".Prepare());
            if (Selected.Cost.Health != 0)
                cursor.NewLine().Print("  ")
                    .Print("жизнь: ".Prepare()).Print(Selected.Cost.Health.ToString());
            if (Selected.Cost.Magic != 0)
                cursor.NewLine().Print("  ")
                    .Print("магия: ".Prepare()).Print(Selected.Cost.Magic.ToString());
            if (Selected.Cost.Stamina != 0)
                cursor.NewLine().Print("  ")
                    .Print("запас сил: ".Prepare()).Print(Selected.Cost.Stamina.ToString());
        }
        
        SadComponents.Add(new LineCharacterFade(TimeSpan.FromMilliseconds(500)));
    }
}