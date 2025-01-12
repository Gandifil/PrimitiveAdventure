namespace PrimitiveAdventure.Core.Rpg.Utils;

public interface ILimitedValue<out T> where T : IComparable<T>
{
    T Value { get; }
    T MaxValue { get; }
}

public class LimitedValue<T> : ILimitedValue<T> where T : IComparable<T>, new()
{
    private T _value;
    public T Value { 
        get => _value; 
        set => _value = Max(new(), Min(MaxValue, value)); }
    public T MaxValue { get; set; }

    public LimitedValue(T initialValue)
    {
        _value = initialValue;
        MaxValue = initialValue;
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