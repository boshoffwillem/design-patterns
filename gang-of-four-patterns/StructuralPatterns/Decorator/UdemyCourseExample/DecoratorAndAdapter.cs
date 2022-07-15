using System.Runtime.Serialization;
using System.Text;

/// <summary>
/// This is real example of how the decorator pattern
/// and the adapter pattern is combined.
///
/// <example>
/// We have the scenario where we want to concatenate strings.
/// <code>
/// var s = "hello ";
/// s += "world";
/// Console.WriteLine(s);
/// </code>
/// But we know this wastes memory and is inefficient.
/// It would be nice if could keep this syntax,
/// but use <see cref="StringBuilder"/> instead.
/// <code>
/// StringBuilder s = "hello ";
/// s += "world";
/// Console.WriteLine(s);
/// </code>
/// </example>
///
/// We have two problems:<br/>
/// <br/>
/// 1.Unfortunately <see cref="StringBuilder"/> does
/// not contain support for implicit conversion from string.<br/>
/// 2.It doesn't support the `+=` operator.<br/>
/// <br/>
/// Because you can inherit from <see cref="StringBuilder"/>
/// we will build a **Decorator** to extend its functionality.
/// But at the same time we will also be building an **Adapter**,
/// because we are making the <see cref="StringBuilder"/> conform
/// (adapting) to an interface that supports implicit string conversion,
/// and that also supports the `+=` operator.
/// </summary>
public class DecoratorAndAdapter
{
    private readonly StringBuilder _builder = new();

    public static implicit operator DecoratorAndAdapter(string s)
    {
        var builder = new DecoratorAndAdapter();
        builder.Append(s);
        return builder;
    }

    public static DecoratorAndAdapter operator +(DecoratorAndAdapter builder,
    string s)
    {
        builder.Append(s);
        return builder;
    }

    public override string ToString()
    {
        return _builder.ToString();
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        ((ISerializable)_builder).GetObjectData(info, context);
    }

    public DecoratorAndAdapter Append(bool value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(byte value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(char value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(char value, int repeatCount)
    {
        _builder.Append(value, repeatCount);
        return this;
    }

    public DecoratorAndAdapter Append(char[]? value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(char[]? value, int startIndex, int charCount)
    {
        _builder.Append(value, startIndex, charCount);
        return this;
    }

    public DecoratorAndAdapter Append(decimal value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(double value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(short value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(int value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(long value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(object? value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(ReadOnlyMemory<char> value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(ReadOnlySpan<char> value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(sbyte value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(float value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(string? value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(string? value, int startIndex, int count)
    {
        _builder.Append(value, startIndex, count);
        return this;
    }

    public DecoratorAndAdapter Append(StringBuilder? value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(StringBuilder? value, int startIndex, int count)
    {
        _builder.Append(value, startIndex, count);
        return this;
    }

    public DecoratorAndAdapter Append(ushort value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(uint value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(ulong value)
    {
        _builder.Append(value);
        return this;
    }

    public DecoratorAndAdapter Append(ref StringBuilder.AppendInterpolatedStringHandler handler)
    {
        _builder.Append(ref handler);
        return this;
    }

    public DecoratorAndAdapter Append(IFormatProvider? provider, ref StringBuilder.AppendInterpolatedStringHandler handler)
    {
        _builder.Append(provider, ref handler);
        return this;
    }

    public DecoratorAndAdapter AppendFormat(IFormatProvider? provider, string format, object? arg0)
    {
        _builder.AppendFormat(provider, format, arg0);
        return this;
    }

    public DecoratorAndAdapter AppendFormat(IFormatProvider? provider, string format, object? arg0, object? arg1)
    {
        _builder.AppendFormat(provider, format, arg0, arg1);
        return this;
    }

    public DecoratorAndAdapter AppendFormat(IFormatProvider? provider, string format, object? arg0, object? arg1, object? arg2)
    {
        _builder.AppendFormat(provider, format, arg0, arg1, arg2);
        return this;
    }

    public DecoratorAndAdapter AppendFormat(IFormatProvider? provider, string format, params object?[] args)
    {
        _builder.AppendFormat(provider, format, args);
        return this;
    }

    public DecoratorAndAdapter AppendFormat(string format, object? arg0)
    {
        _builder.AppendFormat(format, arg0);
        return this;
    }

    public DecoratorAndAdapter AppendFormat(string format, object? arg0, object? arg1)
    {
        _builder.AppendFormat(format, arg0, arg1);
        return this;
    }

    public DecoratorAndAdapter AppendFormat(string format, object? arg0, object? arg1, object? arg2)
    {
        _builder.AppendFormat(format, arg0, arg1, arg2);
        return this;
    }

    public DecoratorAndAdapter AppendFormat(string format, params object?[] args)
    {
        _builder.AppendFormat(format, args);
        return this;
    }

    public DecoratorAndAdapter AppendJoin(char separator, params object?[] values)
    {
        _builder.AppendJoin(separator, values);
        return this;
    }

    public DecoratorAndAdapter AppendJoin(char separator, params string?[] values)
    {
        _builder.AppendJoin(separator, values);
        return this;
    }

    public DecoratorAndAdapter AppendJoin(string? separator, params object?[] values)
    {
        _builder.AppendJoin(separator, values);
        return this;
    }

    public DecoratorAndAdapter AppendJoin(string? separator, params string?[] values)
    {
        _builder.AppendJoin(separator, values);
        return this;
    }

    public DecoratorAndAdapter AppendJoin<T>(char separator, IEnumerable<T> values)
    {
        _builder.AppendJoin(separator, values);
        return this;
    }

    public DecoratorAndAdapter AppendJoin<T>(string? separator, IEnumerable<T> values)
    {
        _builder.AppendJoin(separator, values);
        return this;
    }

    public DecoratorAndAdapter AppendLine()
    {
        _builder.AppendLine();
        return this;
    }

    public DecoratorAndAdapter AppendLine(string? value)
    {
        _builder.AppendLine(value);
        return this;
    }

    public DecoratorAndAdapter AppendLine(ref StringBuilder.AppendInterpolatedStringHandler handler)
    {
        _builder.AppendLine(ref handler);
        return this;
    }

    public DecoratorAndAdapter AppendLine(IFormatProvider? provider, ref StringBuilder.AppendInterpolatedStringHandler handler)
    {
        _builder.AppendLine(provider, ref handler);
        return this;
    }

    public DecoratorAndAdapter Clear()
    {
        _builder.Clear();
        return this;
    }

    public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
    {
        _builder.CopyTo(sourceIndex, destination, destinationIndex, count);
    }

    public void CopyTo(int sourceIndex, Span<char> destination, int count)
    {
        _builder.CopyTo(sourceIndex, destination, count);
    }

    public int EnsureCapacity(int capacity)
    {
        return _builder.EnsureCapacity(capacity);
    }

    public bool Equals(ReadOnlySpan<char> span)
    {
        return _builder.Equals(span);
    }

    public bool Equals(DecoratorAndAdapter? sb)
    {
        return _builder.Equals(sb);
    }

    public StringBuilder.ChunkEnumerator GetChunks()
    {
        return _builder.GetChunks();
    }

    public DecoratorAndAdapter Insert(int index, bool value)
    {
        _builder.Insert(index, value);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, byte value)
    {
        _builder.Insert(index, value);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, char value)
    {
        _builder.Insert(index, value);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, char[]? value)
    {
        _builder.Insert(index, value);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, char[]? value, int startIndex, int charCount)
    {
        _builder.Insert(index, value, startIndex, charCount);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, decimal value)
    {
        _builder.Insert(index, value);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, double value)
    {
        _builder.Insert(index, value);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, short value)
    {
        _builder.Insert(index, value);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, int value)
    {
        _builder.Insert(index, value);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, long value)
    {
        _builder.Insert(index, value);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, object? value)
    {
        _builder.Insert(index, value);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, ReadOnlySpan<char> value)
    {
        _builder.Insert(index, value);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, sbyte value)
    {
        _builder.Insert(index, value);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, float value)
    {
        _builder.Insert(index, value);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, string? value)
    {
        _builder.Insert(index, value);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, string? value, int count)
    {
        _builder.Insert(index, value, count);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, ushort value)
    {
        _builder.Insert(index, value);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, uint value)
    {
        _builder.Insert(index, value);
        return this;
    }

    public DecoratorAndAdapter Insert(int index, ulong value)
    {
        _builder.Insert(index, value);
        return this;
    }

    public DecoratorAndAdapter Remove(int startIndex, int length)
    {
        _builder.Remove(startIndex, length);
        return this;
    }

    public DecoratorAndAdapter Replace(char oldChar, char newChar)
    {
        _builder.Replace(oldChar, newChar);
        return this;
    }

    public DecoratorAndAdapter Replace(char oldChar, char newChar, int startIndex, int count)
    {
        _builder.Replace(oldChar, newChar, startIndex, count);
        return this;
    }

    public DecoratorAndAdapter Replace(string oldValue, string? newValue)
    {
        _builder.Replace(oldValue, newValue);
        return this;
    }

    public DecoratorAndAdapter Replace(string oldValue, string? newValue, int startIndex, int count)
    {
        _builder.Replace(oldValue, newValue, startIndex, count);
        return this;
    }

    public string ToString(int startIndex, int length)
    {
        return _builder.ToString(startIndex, length);
    }

    public int Capacity
    {
        get => _builder.Capacity;
        set => _builder.Capacity = value;
    }

    public char this[int index]
    {
        get => _builder[index];
        set => _builder[index] = value;
    }

    public int Length
    {
        get => _builder.Length;
        set => _builder.Length = value;
    }

    public int MaxCapacity => _builder.MaxCapacity;
}
