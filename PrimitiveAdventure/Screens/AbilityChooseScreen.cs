using PrimitiveAdventure.Core.Rpg.Abilities;
using PrimitiveAdventure.Screens.Base;

namespace PrimitiveAdventure.Screens;

public class AbilityChooseScreen: ChooseScreen<IAbility>
{
    public AbilityChooseScreen(int width, int height, IReadOnlyList<IAbility> elements) : base(width, height, elements)
    {
        var entityView = new AbilityView(width / 2, height - 2)
        {
            Position = (width / 2, 0),
        };
        _entityView = entityView;
        Children.Add(entityView);
    }
}