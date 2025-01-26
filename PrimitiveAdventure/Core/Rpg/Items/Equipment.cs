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
    
    public EventHandler<Kind> Changed;

    public IWeapon Weapon => _items.TryGetValue(Kind.Weapon, out var item) ? (IWeapon)item : Default;

    public void setItem(IItem item)
    {
        _items[item.Kind] = item;
        Changed?.Invoke(this, item.Kind);
    }
    
    public IItem? this[Kind index] => index == Kind.Weapon ? Weapon : _items.GetValueOrDefault(index);
    
    public IReadOnlyDictionary<Kind, IItem> Items => _items;
}