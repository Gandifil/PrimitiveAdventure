namespace PrimitiveAdventure.Core.Rpg.Items;

public class Equipment
{
    public enum Kind
    {
        Weapon, 
        Armor,
        Accessory,
    }

    private static readonly IWeapon Default = new Hands();
    
    private Dictionary<Kind, IItem> _items = new ();
    
    public IWeapon Weapon => _items.TryGetValue(Kind.Weapon, out var item) ? (IWeapon)item : Default;

    public void setItem(IItem item)
    {
        _items[item.Kind] = item;
    }
    
    public IItem? this[Kind index] => index == Kind.Weapon ? Weapon : _items.GetValueOrDefault(index);
}