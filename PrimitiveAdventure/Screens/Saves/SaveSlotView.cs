using PrimitiveAdventure.Screens.Base;

namespace PrimitiveAdventure.Screens.Saves;

public class SaveSlotView: Console, IEntityView<SaveSlot>
{
    public SaveSlotView(int width, int height) : base(width, height)
    {
    }
    
    public void Set(SaveSlot entity)
    {
        Surface.Clear();
        
        Cursor.Position = new Point(0, 0);
        if (entity.Player is null)
            Cursor.Print("Начать игру".Prepare());
        else 
            Cursor.Print(entity.Player.Name.Prepare());
    }
}