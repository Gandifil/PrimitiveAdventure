namespace PrimitiveAdventure.Core.Rpg.Utils;

public interface ILimitedValue<T> where T : IComparable<T>, IEquatable<T>
{
    IObservedValue<T> Value { get; }
    IObservedValue<T> MaxValue { get; }
    float Progress { get; }
    event Action Changed;
}

public class LimitedValue<T> : ILimitedValue<T> where T : IComparable<T>, IEquatable<T>, IConvertible, new()
{
    private ObservedValue<T> _value;

    public T Value { 
        get => _value.Value; 
        set => _value.Value = Max(new(), Min(MaxValue.Value, value)); }

    IObservedValue<T> ILimitedValue<T>.Value { get; }
    IObservedValue<T> ILimitedValue<T>.MaxValue => MaxValue;
    public float Progress => (float)Convert.ToDouble(_value.Value) / (float)Convert.ToDouble(MaxValue.Value);

    public ObservedValue<T> MaxValue { get; }

    public event Action? Changed;

    public LimitedValue(T initialValue)
    {
        _value = new ObservedValue<T>(initialValue);
        MaxValue = new ObservedValue<T>(initialValue);
        
        _value.Changed += (_, _) => Changed?.Invoke();
        MaxValue.Changed += (_, _) => Changed?.Invoke();
    }
    
    static T Min(T x1, T x2)
    {
        int comp=x1.CompareTo(x2);
        if(comp<=0)
            return x1;
        else
            return x2;
    }
    
    static T Max(T x1, T x2)
    {
        int comp=x1.CompareTo(x2);
        if(comp>=0)
            return x1;
        else
            return x2;
    }
}