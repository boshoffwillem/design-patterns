/// <summary>
/// Static composition in C# won't work unfortunately,
/// it's not good enough. The example here is listed
/// to simply demonstrate kind of what static composition
/// would look like, but it is far from complete.
/// <summary/>
public class StaticDecoratorComposition {}

public abstract class ShapeStatic
{
    public abstract string AsString();
}

public class CircleStatic: ShapeStatic
{
    private float _radius;

    public CircleStatic(float radius)
    {
        _radius = radius;
    }

    public CircleStatic() : this(0.0f)
    {
        
    }

    public void Resize(float factor)
    {
        _radius *= factor;
    }

    public override string AsString()
    {
        return $"A circle with radius {_radius}";
    }
}

public class SquareStatic: ShapeStatic
{
    private float _side;

    public SquareStatic(float side)
    {
        _side = side;
    }

    public SquareStatic() : this(0.0f)
    {
        
    }

    public override string AsString()
    {
        return $"A square with side {_side}";
    }
}

public class ColoredShapeStatic: ShapeStatic
{
    private ShapeStatic _shape;
    private string _color;

    public ColoredShapeStatic(ShapeStatic shape, string color)
    {
        _shape = shape;
        _color = color;
    }

    public override string AsString()
    {
        return $"{_shape.AsString()} has the color {_color}";
    }
}

public class TransparentShapeStatic: ShapeStatic
{
    private ShapeStatic _shape;
    private float _transparency;

    public TransparentShapeStatic(ShapeStatic shape, float transparency)
    {
        _shape = shape;
        _transparency = transparency;
    }

    public override string AsString()
    {
        return $"{_shape.AsString()} has {_transparency * 100.0} transparency";
    }
}

public class ColoredShapeStatic<T> : ShapeStatic
    where T : ShapeStatic, new()
{
    private string _color;
    private T _shape = new();

    public ColoredShapeStatic(string color)
    {
        _color = color;
    }

    public ColoredShapeStatic() : this("black")
    {
        
    }

    public override string AsString()
    {
        return $"{_shape.AsString()} has the color {_color}";
    }
}

public class TransparentShapeStatic<T> : ShapeStatic
    where T : ShapeStatic, new()
{
    private float _transparency;
    private T _shape = new();

    public TransparentShapeStatic(float transparency)
    {
        _transparency = transparency;
    }

    public TransparentShapeStatic() : this(0.0f)
    {
        
    }

    public override string AsString()
    {
        return $"{_shape.AsString()} has {_transparency * 100.0f}% transparency";
    }
}
