using PrimitiveAdventure.Core.Rpg.Masteries;
using PrimitiveAdventure.Screens.Base;
using PrimitiveAdventure.Screens.Views;

namespace PrimitiveAdventure.Screens;

public class TalentChooseScreen: ChooseScreen<TalentHandler>
{
    public TalentChooseScreen(IReadOnlyList<TalentHandler> elements) : base(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT, elements, true)
    {
        SetView(new TalentView(Width / 2, Height - 4));
    }
}