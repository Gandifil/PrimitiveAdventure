namespace PrimitiveAdventure.Core.Global;

public interface IGlobalMapCell
{
    string RenderName { get; }
    
    string? Resource { get; }

    void OnCollisionWith(Player player)
    {
    }
}