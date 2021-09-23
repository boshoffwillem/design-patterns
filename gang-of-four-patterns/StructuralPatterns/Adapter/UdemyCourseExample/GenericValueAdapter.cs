using System;

namespace UdemyCourseExample
{
    /// <summary>
    /// Let's suppose you want to implement vectors;
    /// <br />you want to have the idea of a vector.
    /// <br />A vector can have different dimensions (2D, 3D, etc.)
    /// <br />A vector can be of different types (int, float, etc.)
    /// <br />We want some generic approach to implement all of this.
    /// <br />In the en we want something like Vector2f (2 dimensions of type float), Vector3i (3 dimensions of type int.)
    /// </summary>
    public class GenericValueAdapter
    {
        public GenericValueAdapter()
        {
            Vector2i vector2i_1 = Vector2i.Create(1, 2);
            Vector2i vector2i_2 = Vector2i.Create(3, 2);
        }
    }

    #region The Adapter

    /// <summary>
    /// Step 1, we make abstract classes or interfaces that just
    /// yields the literal value.
    /// </summary>

    public interface IInteger
    {
        int Value { get; }
    }

    public static class Dimensions
    {
        public class Two : IInteger
        {
            public int Value => 2;
        }

        public class Three : IInteger
        {
            public int Value => 3;
        }
    }
    #endregion

    #region The Generic Vector

    /// <summary>
    /// The generic vector.
    /// </summary>
    /// <param name="T">The data type of the vector</param>
    /// <param name="D">The dimensions of the vector</param>
    public class Vector<TSelf, T, D>
        where D : IInteger, new()
        where TSelf : Vector<TSelf, T, D>, new()
    {
        protected T[] Data;

        public Vector()
        {
            Data = new T[new D().Value];
        }

        public Vector(params T[] values)
        {
            int requiredDimensions = new D().Value;
            Data = new T[requiredDimensions];

            int providedDimensions = values.Length;

            for (int i = 0; i < Math.Min(requiredDimensions, providedDimensions); ++i)
            {
                Data[i] = values[i];
            }
        }

        public static TSelf Create(params T[] values)
        {
            TSelf result = new();
            int requiredDimensions = new D().Value;
            result.Data = new T[requiredDimensions];

            int providedDimensions = values.Length;

            for (int i = 0; i < Math.Min(requiredDimensions, providedDimensions); ++i)
            {
                result.Data[i] = values[i];
            }

            return result;
        }

        public T this[int index]
        {
            get => Data[index];
            set => Data[index] = value;
        }
    }

    #endregion

    #region Custom Vectors

    public class VectorOfInt<TSelf, D> : Vector<TSelf, int, D>
        where D : IInteger, new()
        where TSelf : Vector<TSelf, int, D>, new()
    {
    }

    public class VectorOfFloat<TSelf, D> : Vector<TSelf, float, D>
        where D : IInteger, new()
        where TSelf : Vector<TSelf, float, D>, new()
    {
    }

    public class Vector2i : VectorOfInt<Vector2i, Dimensions.Two>
    {
    }

    public class Vector3f : VectorOfFloat<Vector3f, Dimensions.Three>
    {
    }

    #endregion
}
