using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Fight;
using PrimitiveAdventure.SadConsole;
using PrimitiveAdventure.Screens;
using PrimitiveAdventure.Screens.Fight;
using PrimitiveAdventure.Ui;

namespace PrimitiveAdventure.Core.Global;

public class EnemyGroup: IGlobalMapCell
{
    public List<Actor> Enemies { get; } = new();

    public IScreenObject View
    {
        get
        {
            var animation = Animations.Noise(MapScreen.CELL_WIDTH - 1, MapScreen.CELL_HEIGHT - 1, Color.Red);
            animation.Start();
            return animation;
        }
    }

    public void OnCollisionWith(Player player)
    {
        var builder = new FightBuilder();
        builder.AddPlayer(player);
        builder.Add(Enemies);
        builder.Build().Start();
    }
}