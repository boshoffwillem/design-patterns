using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Collections;

namespace UdemyCourseExample
{
    /// <summary>
    /// Suppose we a making a simple drawing application.
    /// <br />Let's suppose the only thing our system knows how to do is to draw pixels (<seealso cref="Point"/>s on the screen).
    /// <br />So the only interface we is that we can draw a <seealso cref="Point"/>.
    /// <br />
    /// <br />Let's suppose somewhere else in the system someone has implemented Vector drawing functionality.
    /// <br />For example, drawing a <seealso cref="Line"/> or some object (<seealso cref="VectorObject"/>, <seealso cref="VectorRectangle"/>).
    /// <br />
    /// <br />Let's suppose they decide to give us a bunch of these <seealso cref="VectorObject"/>s.
    /// <br />And they ask that we draw it for them.
    /// <br />
    /// <br />So the question is how can we then adapt a vector to a point. We need an adapter that allows us
    /// to go from vector drawing to raster drawing.
    /// <br />
    /// <br />This is where we build an Adapter (<seealso cref="LineToPointAdapter"/>). In this case something that converts
    /// a line, or vector, into a set of points, because that is essentially what a vector is.
    /// <br />
    /// <br />
    /// <br />
    /// <br />
    /// <br />
    /// <br />
    /// <br />
    /// <br />
    /// <br />
    /// <br />
    /// </summary>
    public class Vector_Raster_Example
    {
        public static readonly List<VectorObject> VectorObjects =
            new()
            {
                new VectorRectangle(1, 1, 10, 10),
                new VectorRectangle(3, 3, 6, 6),
            };

        public static void DrawPoint(Point p)
        {
            Console.Write(".");
        }
    }

    public class Point
    {
        public int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        protected bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }

    public class Line
    {
        public Point Start;
        public Point End;

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        protected bool Equals(Line other)
        {
            return Equals(Start, other.Start) && Equals(End, other.End);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Line) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Start != null ? Start.GetHashCode() : 0) * 397) ^ (End != null ? End.GetHashCode() : 0);
            }
        }
    }

    public class VectorObject : Collection<Line>
    {

    }

    public class VectorRectangle : VectorObject
    {
        public VectorRectangle(int x, int y, int width, int height)
        {
            Add(new Line(new Point(x, y), new Point(x + width, y)));
            Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
            Add(new Line(new Point(x, y), new Point(x, y + height)));
            Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
        }
    }

    public class LineToPointAdapter : IEnumerable<Point>
    {
        /// <summary>
        /// Counter for number of line-to-point conversions.
        /// </summary>
        private static int _count = 0;
        private static readonly Dictionary<int, List<Point>> _cache = new();
        private readonly int _hash;

        public LineToPointAdapter(Line line)
        {
            _hash = line.GetHashCode();
            if (_cache.ContainsKey(_hash)) return; // we already have it

            Console.WriteLine($"{++_count}: Generating points for line [{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}]");

            List<Point> points = new();

            int left = Math.Min(line.Start.X, line.End.X);
            int right = Math.Max(line.Start.X, line.End.X);
            int top = Math.Min(line.Start.Y, line.End.Y);
            int bottom = Math.Max(line.Start.Y, line.End.Y);
            int dx = right - left;
            int dy = line.End.Y - line.Start.Y;

            if (dx == 0)
            {
                for (int y = top; y <= bottom; ++y)
                {
                    points.Add(new Point(left, y));
                }
            }
            else if (dy == 0)
            {
                for (int x = left; x <= right; ++x)
                {
                    points.Add(new Point(x, top));
                }
            }

            _cache.Add(_hash, points);
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return _cache.Values.SelectMany(x => x).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
