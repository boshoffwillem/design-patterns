using System.Collections.Generic;
using System.Text;

namespace UdemyCourseExample
{
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new();
        private const int _indentSize = 2;

        public HtmlElement()
        {

        }

        public HtmlElement(string name, string text)
        {
            Name = name;
            Text = text;
        }

        private string ToStringImplementation(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indent * _indentSize);
            sb.AppendLine($"{i}<{Name}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', (indent + 1) * _indentSize));
                sb.AppendLine(Text);
            }

            foreach (var element in Elements)
            {
                sb.Append(element.ToStringImplementation(indent + 1));
            }

            sb.AppendLine($"{i}</{Name}>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImplementation(0);
        }
    }

    public class HtmlBuilder
    {
        private HtmlElement _root = new();
        private readonly string _rootName;

        public HtmlBuilder(string rootName)
        {
            _rootName = rootName;
            _root.Name = _rootName;
        }

        public HtmlBuilder AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            _root.Elements.Add(e);
            return this;
        }

        public override string ToString()
        {
            return _root.ToString();
        }

        public void Clear()
        {
            _root = new HtmlElement
            {
                Name = _rootName
            };
        }
    }
}