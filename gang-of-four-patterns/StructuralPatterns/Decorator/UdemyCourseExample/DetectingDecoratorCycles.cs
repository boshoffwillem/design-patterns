public class DetectingDecoratorCycles{}

public abstract class ShapeDecoratorCyclePolicy
{
    public abstract bool TypeAdditionAllowed(Type type, IList<Type> allTypes);
    public abstract bool ApplicationAllowed(Type type, IList<Type> allTypes);
}

public class ThrowOnCyclePolicy : ShapeDecoratorCyclePolicy
{
    private bool Handler(Type type, IList<Type> allTypes)
    {
        if (allTypes.Contains(type))
            throw new InvalidOperationException($"Cycle detected! Type is already a {type.FullName}.");
        return true;
    }

    public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
    {
        return Handler(type, allTypes);
    }

    public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
    {
        return Handler(type, allTypes);
    }
}

public abstract class ShapeDecorator : Shape
{
    protected internal readonly List<Type> Types = new();
    protected internal Shape Shape;

    public ShapeDecorator(Shape shape)
    {
        Shape = shape;

        if (shape is ShapeDecorator sd)
            Types.AddRange(sd.Types);
    }
}

public abstract class ShapeDecorator<TSelf, TCyclePolicy> : ShapeDecorator
    where TCyclePolicy : ShapeDecoratorCyclePolicy, new()
{
    protected readonly TCyclePolicy Policy = new();

    protected ShapeDecorator(Shape shape) : base(shape)
    {
        if (Policy.TypeAdditionAllowed(typeof(TSelf), Types))
            Types.Add(typeof(TSelf));
    }
}

public abstract class Shape
{
    public abstract string AsString();
}

public class SquareCycle : Shape
{
    private float _side;

    public SquareCycle(float side)
    {
        _side = side;
    }

    public void Resize(float factor)
    {
        _side *= factor;
    }

    public override string AsString()
    {
        return $"Square with side {_side}";
    }
}

/// <summary>
/// This decorator is dynamic, because the decorator is an
/// interface instead of a concrete class.
/// </summary>
public class ColoredShapeDecoratorCycle
    : ShapeDecorator<ColoredShapeDecoratorCycle, ThrowOnCyclePolicy>
{
    private string _color;

    public ColoredShapeDecoratorCycle(Shape shape, string color)
        : base(shape)
    {
        Shape = shape;
        _color = color;
    }

    public override string AsString()
    {
        return $"{Shape.AsString()} has the color {_color}";
    }
}

public class TransparentShapeDecoratorCycle : Shape
{
    private Shape _shape;
    private float _transparency;

    public TransparentShapeDecoratorCycle(Shape shape, float transparency)
    {
        _shape = shape;
        _transparency = transparency;
    }

    public override string AsString()
    {
        return $"{_shape.AsString()} has {_transparency * 100.0}% transparency";
    }
}
