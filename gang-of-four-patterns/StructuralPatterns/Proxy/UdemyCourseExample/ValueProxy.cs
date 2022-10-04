/// <summary>
/// A value proxy is typically a proxy that
/// is constructed over a primitive type.
/// Why would you want a proxy over something like an
/// int or float?
///
/// One reason is for stronger typing.
/// Let's say we have some method Foo that takes an integer.
/// But, that integer can only represent a price in dollars.
/// How would we enforce that?
/// </summary>
public class ValueProxy
{
}

/// <summary>
/// Let's look at a real-world example where
/// we want to have the concept of a percentage value.
/// We want to be able to do things like,
/// Console.WriteLine(10f + 5.Percent())
/// or Console.WriteLine(2.Percent() + 3.Percent()).
/// </summary>
public struct Percentage
{
    private readonly float _value;

    public Percentage(float value)
    {
        _value = value;
    }

    public static float operator *(float f, Percentage p)
    {
        return f * p._value;
    }

    public static Percentage operator +(Percentage a, Percentage b)
    {
        return new Percentage(a._value + b._value);
    }

    public override string ToString()
    {
        return $"{_value * 100 }%";
    }
}

public static class PercentageExtensions
{
    public static Percentage Percent(this int value)
    {
        return new Percentage(value / 100.0f);
    }

    public static Percentage Percent(this float value)
    {
        return new Percentage(value / 100.0f);
    }
}
