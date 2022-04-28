/// <summary>
/// Dynamic decorators mean that the decorators
/// are made at runtime instead of at compile time.
/// </summary>
public class DynamicDecoratorComposition{}

public interface IShape
{
    string AsString();
}

public class Circle : IShape
{
    private float _radius;

    public Circle(float radius)
    {
        _radius = radius;
    }

    public void Resize(float factor)
    {
        _radius *= factor;
    }

    public string AsString()
    {
        return $"Circle with radius {_radius}";
    }
}

public class Square : IShape
{
    private float _side;

    public Square(float side)
    {
        _side = side;
    }

    public void Resize(float factor)
    {
        _side *= factor;
    }

    public string AsString()
    {
        return $"Square with side {_side}";
    }
}

/// <summary>
/// This decorator is dynamic, because the decorator is an
/// interface instead of a concrete class.
/// </summary>
public class ColoredShapeDecorator : IShape
{
    private IShape _shape;
    private string _color;

    public ColoredShapeDecorator(IShape shape, string color)
    {
        _shape = shape;
        _color = color;
    }

    public string AsString()
    {
        return $"{_shape.AsString()} has the color {_color}";
    }
}

public class TransparentShapeDecorator : IShape
{
    private IShape _shape;
    private float _transparency;

    public TransparentShapeDecorator(IShape shape, float transparency)
    {
        _shape = shape;
        _transparency = transparency;
    }

    public string AsString()
    {
        return $"{_shape.AsString()} has {_transparency * 100.0}% transparency";
    }
}
