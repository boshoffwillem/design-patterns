/// <summary>
/// This approach will be costly as the amount of users
/// grows. Because every user will have unique allocations
/// for their names, even though there might be multiple
/// duplicate names.
/// </summary>
public class User
{
    private string _fullName;

    public User(string fullName)
    {
        _fullName = fullName;
    }
}

/// <summary>
/// This approach removes the duplication
/// by simply having a list of unique names.
/// Every name is then referred to by an index.
/// </summary>
public class UserFlyweight
{
    private static List<string> _strings = new();
    private int[] _names;

    public UserFlyweight(string fullName)
    {
        int GetOrAdd(string s)
        {
            int idx = _strings.IndexOf(s);
            if (idx != -1) return idx;
            else
            {
                _strings.Add(s);
                return _strings.Count - 1;
            }
        }

        _names = fullName.Split(' ')
            .Select(GetOrAdd).ToArray();
    }

    public string FullName => string.Join(" ",
        _names.Select(i => _strings[i]));
}