namespace PrimitiveAdventure.Core.Global;

public interface IGlobalMapCell
{
    IScreenObject View { get; }

    void OnCollisionWith(Player player)
    {
    }
}