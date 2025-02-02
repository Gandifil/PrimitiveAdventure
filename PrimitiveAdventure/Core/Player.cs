using System.Collections.ObjectModel;
using PrimitiveAdventure.Core.Global;
using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Abilities;
using PrimitiveAdventure.Core.Rpg.Controlling;
using PrimitiveAdventure.Core.Rpg.Items;
using PrimitiveAdventure.Core.Rpg.Masteries;
using PrimitiveAdventure.Utils;

namespace PrimitiveAdventure.Core;

public interface IPlayer: IActor, IGlobalMapCell
{
    public Point GlobalPosition { get; }
    
    public IActorController Control { get; }
    
    public Equipment Equipment { get; }
    
    public MasteryManager Masteries { get; }
}

public class Player: Actor, IPlayer, ISaveable
{
    public Point GlobalPosition { get; set; }
    public IActorController Control => (PlayerController)Controller;
    public Equipment Equipment { get; } = new();
    public MasteryManager Masteries { get; }
    public string RenderName { get; } = "player";
    public string? Resource { get; } = "player";

    public override int Damage => Equipment.Weapon.Damage;

    public Player()
    {
        Name = "Player";
        Direction = 1;
        Abilities.Add(new SelfHeal());
        Abilities.Add(new DoubleStrike());
        Abilities.ForEach(x => x.SetOwner(this));

        Masteries = new(this);
        Equipment.Changed += (sender, kind) => UpdateParameters();
    }

    private void UpdateParameters()
    {

    }

    public void Save(StreamWriter streamWriter)
    {
        throw new NotImplementedException();
    }

    public void Load(StreamReader streamReader)
    {
        throw new NotImplementedException();
    }

    public int Level { get; private set; } = 1;

    public int MasteryPoints { get; private set; } = 0;

    public void LevelUp(int value)
    {
        Level += value;
        MasteryPoints = 1 + value / 3;
    }
}