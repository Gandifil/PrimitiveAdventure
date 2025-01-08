namespace PrimitiveAdventure.Core.Rpg.Utils;

public interface ILimitedValue<out T> where T : IComparable<T>
{
    T Value { get; }
    T MaxValue { get; }
}

public class LimitedValue<T> : ILimitedValue<T> where T : IComparable<T>
{
    public T Value { get; set; }
    public T MaxValue { get; set; }

    public LimitedValue(T initialValue)
    {
        Value = initialValue;
        MaxValue = initialValue;
    }
}