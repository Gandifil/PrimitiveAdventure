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
        Game.Instance.Screen.IsFocused = false;
        Game.Instance.Screen = new FightScreen(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT,
            new FightProcess(player, Enemies));
        Game.Instance.Screen.IsFocused = true;
        
    }
}