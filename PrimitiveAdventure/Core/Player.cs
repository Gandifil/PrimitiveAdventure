using System.Collections.ObjectModel;
using PrimitiveAdventure.Core.Global;
using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Abilities;
using PrimitiveAdventure.Core.Rpg.Controlling;
using PrimitiveAdventure.Core.Rpg.Items;
using PrimitiveAdventure.Core.Rpg.Masteries;

namespace PrimitiveAdventure.Core;

public interface IPlayer: IActor, IGlobalMapCell
{
    public Point GlobalPosition { get; }
    
    public IActorController Control { get; }
    
    public Equipment Equipment { get; }
    
    public MasteryManager Masteries { get; }
}

public class Player: Actor, IPlayer
{
    public Point GlobalPosition { get; set; }
    public IActorController Control => (PlayerController)Controller;
    public Equipment Equipment { get; } = new();
    public MasteryManager Masteries { get; }
    public string Name { get; } = "player";
    public string? Resource { get; } = "player";

    public override int Damage => Equipment.Weapon.Damage;

    public Player()
    {
        Direction = 1;
        Abilities.Add(new SelfHeal(this));
        Abilities.Add(new DoubleStrike(this));

        Masteries = new(this);
        Equipment.Changed += (sender, kind) => UpdateParameters();
    }

    private void UpdateParameters()
    {

    }
}