using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Fight;
using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.Screens;
using PrimitiveAdventure.Screens.Fight;

namespace PrimitiveAdventure.Core.Global;

public class EnemyGroup: IGlobalMapCell
{
    public string RenderName { get; } = null;
    public string? Resource { get; } = null;

    public List<Actor> Enemies { get; } = new();

    public void OnCollisionWith(Player player)
    {
        var builder = new FightBuilder();
        builder.AddPlayer(player);
        builder.Add(Enemies);
        builder.Build().Start();
    }
}