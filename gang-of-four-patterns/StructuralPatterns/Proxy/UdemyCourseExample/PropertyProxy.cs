/// <summary>
/// The idea behind a property proxy is to use
/// and object as the property instead of some
/// literal value.
/// <summary>
public class PropertyProxy
{
}

/// <summary>
/// This is going to be the type that's going to encapsulate
/// a property of type `T`, and it's going to keep it as an
/// object.
///
/// This property proxy has simple task of preventing
/// duplicate assignments.
/// <summary>
public class Property<T> where T : new()
{
    private T _value;

    public T Value
    { 
        get => _value;
        set
        {
            if (Equals(_value, value)) return;
            Console.WriteLine($"Assigning value to {value}");
            _value = value;
        }
    }

    public Property() : this(default(T))
    {
    }

    public Property(T value)
    {
        _value = value;
    }

    public static implicit operator T(Property<T> property)
    {
        return property.Value;
    }

    public static implicit operator Property<T>(T value)
    {
        return new Property<T>(value);
    }
}


public class Creature
{
    private Property<int> _agility = new();

    public int Agility
    {
        get => _agility.Value;
        set => _agility.Value = value;
    }
}
