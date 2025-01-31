using PrimitiveAdventure.Core;
using PrimitiveAdventure.Utils;

namespace PrimitiveAdventure.Screens.Saves;

public record SaveSlot(int Index)
{
    public Player? Player { get; set; } = Content.Saves.Load("save" + Index);

    public override string ToString()
    {
        return (Player?.Name ?? $"слот{Index}").Prepare();
    }
}