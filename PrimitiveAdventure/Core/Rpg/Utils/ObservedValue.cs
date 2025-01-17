namespace PrimitiveAdventure.Core.Rpg.Utils;

public interface IObservedValue<T> where T : IEquatable<T>
{
    T Value { get; }
    
    event ObservedValue<T>.ChangeCallback Changed;
}

public class ObservedValue<T> : IObservedValue<T> where T:IEquatable<T>
{
    private T _value;

    public T Value
    {
        get { return _value; }
        set
        {
            if (_value.Equals(value)) return;
            var old = _value;
            _value = value;
            Changed?.Invoke(value, old);
        }
    }

    public ObservedValue()
    {
            
    }

    public ObservedValue(T x)
    {
        _value = x;
    }

    public delegate void ChangeCallback(T newValue, T oldValue);
    
    public event ChangeCallback Changed;

    public static implicit operator T(ObservedValue<T> p)
    {
        return p._value;
    }

    public override string? ToString()
    {
        return Value.ToString();
    }
}