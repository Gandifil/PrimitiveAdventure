using System.Collections.ObjectModel;
using PrimitiveAdventure.Core.Global;
using PrimitiveAdventure.Core.Rpg;
using PrimitiveAdventure.Core.Rpg.Abilities;
using PrimitiveAdventure.Core.Rpg.Controlling;
using PrimitiveAdventure.Core.Rpg.Items;
using PrimitiveAdventure.Core.Rpg.Masteries;
using PrimitiveAdventure.Screens;
using PrimitiveAdventure.Screens.Views;
using PrimitiveAdventure.Utils;

namespace PrimitiveAdventure.Core;

public interface IPlayer: IActor, IGlobalMapCell
{
    public Point GlobalPosition { get; }
    
    public IActorController Control { get; }
    
    public Equipment Equipment { get; }
    
    public MasteryManager Masteries { get; }

    void LevelUp(int value = 1);
}

public class Player: Actor, IPlayer, ISaveable
{
    public static string RESOURCE_NAME = "player";

    public IScreenObject View => new ResourceView(MapScreen.CELL_WIDTH, MapScreen.CELL_HEIGHT, RESOURCE_NAME);
    
    public Point GlobalPosition { get; set; }
    public IActorController Control => (PlayerController)Controller;
    public Equipment Equipment { get; } = new();
    public MasteryManager Masteries { get; }

    public override int Damage => Equipment.Weapon.Damage;

    public Player()
    {
        Name = "Player";

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

    public int MasteryPoints { get; private set; } = 1;

    public void LevelUp(int value = 1)
    {
        Level += value;
        MasteryPoints = 1 + Level / 3;
    }
}