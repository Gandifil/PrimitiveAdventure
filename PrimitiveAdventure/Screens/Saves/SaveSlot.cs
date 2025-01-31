using PrimitiveAdventure.Core;
using PrimitiveAdventure.Utils;

namespace PrimitiveAdventure.Screens.Saves;

public class SaveSlot
{
    private readonly int _index;

    public Player? Player { get; set; }

    public SaveSlot(int index)
    {
        _index = index;
        
        Player = Content.Saves.Load("save" + index);
    }
    
    public override string ToString()
    {
        return (Player?.Name ?? $"слот{_index}").Prepare();
    }
}