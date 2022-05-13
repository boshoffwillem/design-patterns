using System.Text;

/// <summary>
/// While this approach makes sense we're spending
/// a lot of memory on flags that don't need to be
/// there. The problem increases if start adding
/// flags for bold and italic.
/// </summary>
public class FormattedText
{
    private readonly string _plainText;
    private bool[] _capitalize;
    
    public FormattedText(string plainText)
    {
        _plainText = plainText;
        _capitalize = new bool[_plainText.Length];
    }

    public void Capitalize(int start, int end)
    {
        for (var i = start; i < end; i++)
            _capitalize[i] = true;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < _plainText.Length; i++)
        {
            var c = _plainText[i];
            sb.Append(_capitalize[i]
                ? char.ToUpper(c)
                : c);
        }

        return sb.ToString();
    }
}

/// <summary>
/// A better approach would be to have
/// a bunch of ranges.
/// </summary>
public class FormattedTextFlyweight
{
    private readonly string _plainText;
    private List<TextRange> _formatting = new();
    
    public FormattedTextFlyweight(string plainText)
    {
        _plainText = plainText;
    }

    public class TextRange
    {
        public int Start { get; set; }
        public int End { get; set; }
        public bool Capitalize { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }

        public bool Covers(int position)
        {
            return position >= Start
                   && position <= End;
        }
    }

    public TextRange GetRange(int start, int end)
    {
        var range = new TextRange
        {
            Start = start,
            End = end
        };
        _formatting.Add(range);
        return range;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < _plainText.Length; i++)
        {
            var c = _plainText[i];
            
            foreach (var range in _formatting)
                if (range.Covers(i) && range.Capitalize)
                    c = char.ToUpper(c);

            sb.Append(c);
        }

        return sb.ToString();
    }
}
