using PrimitiveAdventure.SadConsole;

namespace PrimitiveAdventure.Screens.Saves;

public class SaveChooseScreen: ChooseScreen<SaveSlot>
{
    public SaveChooseScreen() 
        : base(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT, GetAllSaveSlots(), true)
    {
        var entityView = new SaveSlotView(Width / 2, Height - 2)
        {
            Position = (Width / 2, 0),
        };
        _entityView = entityView;
        Children.Add(entityView);

        SelectedSuccessfully += slot => (slot.Player is null ? new CreatePlayerScreen(slot.Index) : null!).Start();
    }

    private static IReadOnlyList<SaveSlot> GetAllSaveSlots()
    {
        return Enumerable.Range(0, 3).Select(x => new SaveSlot(x)).ToList();
    }
}