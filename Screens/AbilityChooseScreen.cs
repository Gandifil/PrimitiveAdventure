using PrimitiveAdventure.Core.Rpg.Abilities;

namespace PrimitiveAdventure.Screens;

public class AbilityChooseScreen: ChooseScreen<IAbility>
{
    private readonly Console _descriptionSurface;
    
    public AbilityChooseScreen(int width, int height, IReadOnlyList<IAbility> elements) : base(width, height, elements)
    {
        Children.Add(_descriptionSurface = new Console(width / 2, height - 2));
        _descriptionSurface.Position = new Point(width / 2, 0);
    }

    protected override void UpdateDescription()
    {
        _descriptionSurface.Clear();

        var cursor = _descriptionSurface.Cursor;
        cursor.Position = new Point(0, 0);
        cursor.NewLine().Print(Selected.Description.Prepare());

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

    }
}