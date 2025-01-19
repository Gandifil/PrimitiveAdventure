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
}

public class Sword: IWeapon
{
    public int Damage => 4;
    public string Name => "Меч";
    public Equipment.Kind Kind => Equipment.Kind.Weapon;
    public string ResourceName => "items/sword";
}

