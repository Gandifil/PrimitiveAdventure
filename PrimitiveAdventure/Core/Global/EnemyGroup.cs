using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Screens;

namespace PrimitiveAdventure.Core.Global;

public class EnemyGroup: IGlobalMapCell
{
    public string Name { get; } = null;
    public string? Resource { get; } = null;

    public List<Actor> Enemies { get; } = new();

    public void OnCollisionWith(Player player)
    {
        var fightProcess = new FightProcess(player, Enemies);

        new FightScreen(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT, fightProcess)
            .Start();
    }
}