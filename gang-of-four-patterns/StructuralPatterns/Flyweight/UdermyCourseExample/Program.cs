using JetBrains.dotMemoryUnit;
using Xunit;
using Xunit.Abstractions;

public class Flyweight
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Flyweight(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        DotMemoryUnitTestOutput.SetOutputMethod(_testOutputHelper.WriteLine);
    }

    [Fact]
    [DotMemoryUnit(FailIfRunWithoutSupport = false)]
    public void TestUser()
    {
        var firstNames = Enumerable.Range(0, 100)
            .Select(_ => RandomString());
        var lastNames = Enumerable.Range(0, 100)
            .Select(_ => RandomString())
            .ToArray();
        var users = new List<User>();

        foreach (var firstName in firstNames)
            foreach (var lastName in lastNames)
                users.Add(new User($"{firstName} {lastName}"));

        ForceGC();
        dotMemory.Check(memory =>
        {
            _testOutputHelper.WriteLine(memory.SizeInBytes.ToString());
        });
    }
    
    [Fact]
    [DotMemoryUnit(FailIfRunWithoutSupport = false)]
    public void TestUserFlyweight()
    {
        var firstNames = Enumerable.Range(0, 100)
            .Select(_ => RandomString());
        var lastNames = Enumerable.Range(0, 100)
            .Select(_ => RandomString())
            .ToArray();
        var users = new List<UserFlyweight>();

        foreach (var firstName in firstNames)
            foreach (var lastName in lastNames)
                users.Add(new UserFlyweight($"{firstName} {lastName}"));

        ForceGC();
        dotMemory.Check(memory =>
        {
            _testOutputHelper.WriteLine(memory.SizeInBytes.ToString());
        });
    }

    private void ForceGC()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }

    private static string RandomString()
    {
        Random rand = new();
        return new string(
            Enumerable.Range(0, 10)
            .Select(i => (char)('a' + rand.Next(26)))
            .ToArray());
    }

    [Fact]
    public void TextFormatting()
    {
        var ft = new FormattedText("This is a brave new world");
        ft.Capitalize(10, 15); // brave
        _testOutputHelper.WriteLine(ft.ToString());
    }
    
    [Fact]
    public void TextFormattingFlyweight()
    {
        var ft = new FormattedTextFlyweight("This is a brave new world");
        ft.GetRange(10, 15)
            .Capitalize = true; // brave
        _testOutputHelper.WriteLine(ft.ToString());
    }
}
