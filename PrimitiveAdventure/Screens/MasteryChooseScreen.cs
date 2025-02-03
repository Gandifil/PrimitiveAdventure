using PrimitiveAdventure.Core;
using PrimitiveAdventure.Core.Rpg.Masteries;
using PrimitiveAdventure.Screens.Base;
using PrimitiveAdventure.Screens.Views;

namespace PrimitiveAdventure.Screens;

public class MasteryChooseScreen: ChooseScreen<IMastery>
{
    private readonly MasteryManager _manager;

    public MasteryChooseScreen(MasteryManager manager) 
        : base(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT, manager.GetFreeMastery(), false)
    {
        _manager = manager;
        var entityView = new MasteryView(Width / 2, Height - 2)
        {
            Position = (Width / 2, 0),
        };
        _entityView = entityView;
        Children.Add(entityView);
    }
}