using PrimitiveAdventure.Core.Global;
using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Abilities;
using PrimitiveAdventure.Core.Rpg.Controlling;
using PrimitiveAdventure.Core.Rpg.Items;

namespace PrimitiveAdventure.Core;

public interface IPlayer: IActor, IGlobalMapCell
{
    public Point GlobalPosition { get; }
    
    public IActorController Control { get; }
    
    public Equipment Equipment { get; }
}

public class Player: Actor, IPlayer
{
    public Point GlobalPosition { get; set; }
    public IActorController Control => (PlayerController)Controller;
    public Equipment Equipment { get; } = new();
    public string Name { get; } = "player";
    public string? Resource { get; } = "player";

    public override int Damage => Equipment.Weapon.Damage;

    public Player()
    {
        Direction = 1;
        Abilities.Add(new SelfHeal(this));
        Abilities.Add(new DoubleStrike(this));
    }
}