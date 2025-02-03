using PrimitiveAdventure.Core;
using PrimitiveAdventure.Core.Rpg.Masteries;
using PrimitiveAdventure.Screens.Base;
using PrimitiveAdventure.Screens.Views;

namespace PrimitiveAdventure.Screens;

public class MasteryChooseScreen: ChooseScreen<IMastery>
{
    public MasteryChooseScreen(IReadOnlyList<IMastery> masteries) 
        : base(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT, masteries, false)
    {
        var entityView = new MasteryView(Width / 2, Height - 2)
        {
            Position = (Width / 2, 0),
        };
        _entityView = entityView;
        Children.Add(entityView);
    }
}