using PrimitiveAdventure.Core.Rpg.Abilities;
using PrimitiveAdventure.Screens.Base;
using PrimitiveAdventure.Screens.Views;

namespace PrimitiveAdventure.Screens;

public class AbilityChooseScreen: ChooseScreen<IAbility>
{
    public AbilityChooseScreen(IReadOnlyList<IAbility> elements) : base(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT, elements)
    {
        var entityView = new AbilityView(Width / 2, Height - 2)
        {
            Position = (Width / 2, 0),
        };
        _entityView = entityView;
        Children.Add(entityView);
    }

    protected override bool GetEnterIsEnabled()
    {
        return Selected!.IsUsableBy();
    }
}