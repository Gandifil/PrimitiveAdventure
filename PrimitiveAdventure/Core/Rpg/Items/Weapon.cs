using PrimitiveAdventure.Core.Rpg.Modifiers;

namespace PrimitiveAdventure.Core.Rpg.Items;

public interface IWeapon: IItem
{
    int Damage { get; }
}

public class Hands: IWeapon
{
    public int Damage => 2;
    public string Name => "Руки";
    public Equipment.Kind Kind => Equipment.Kind.Weapon;
    public string ResourceName => "items/hands";

    public IReadOnlyList<IModifier> Modifiers { get; } = new List<IModifier>()
    {
        
    };
}

public class Sword: IWeapon
{
    public int Damage => 4;
    public string Name => "Меч";
    public Equipment.Kind Kind => Equipment.Kind.Weapon;
    public string ResourceName => "items/sword";

    public IReadOnlyList<IModifier> Modifiers { get; } = new List<IModifier>()
    {
        new ParamMod(Parameters.Kind.CounterAttackRate, 10)
    };
}

