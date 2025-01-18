namespace PrimitiveAdventure.Core.Rpg.Items;

public interface IWeapon: IItem
{
    int Damage { get; }
}

public class Hands: IWeapon
{
    public int Damage => 2;
}

public class Sword: IWeapon
{
    public int Damage => 4;
}

