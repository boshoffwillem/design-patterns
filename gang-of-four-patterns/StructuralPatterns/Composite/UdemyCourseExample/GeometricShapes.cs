using System;
using System.Collections.Generic;
using System.Text;

namespace Composite
{
    /*
     * In this example we emulate a drawing application.
     * In drawing application you can create different shapes
     * but you can also group shapes together.
     * And if you group shapes together you can drag that entire group.
     * We are going to implement this using the composite desgin pattern.
     */
    public class GraphicObject
    {
        private Lazy<List<GraphicObject>> _children = new();

        public virtual string Name { get; set; } = "Group";

        public string Color { get; set; }

        public List<GraphicObject> Children => _children.Value;

        private void Print(StringBuilder stringBuilder, int depth)
        {
            stringBuilder.Append(new string('*', depth))
                .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ")
                .AppendLine(Name);

            foreach (var child in Children)
            {
                child.Print(stringBuilder, depth + 1);
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            Print(stringBuilder, 0);
            return stringBuilder.ToString();
        }
    }

    public class Circle : GraphicObject
    {
        public override string Name => "Circle";
    }

    public class Square : GraphicObject
    {
        public override string Name => "Square";
    }
}
