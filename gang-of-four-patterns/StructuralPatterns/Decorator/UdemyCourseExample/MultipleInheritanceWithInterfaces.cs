public interface IBird
{
    int Weight { get; set; }
    void Fly();
}

public class Bird: IBird
{
    public int Weight { get; set; }

    public void Fly()
    {
        Console.WriteLine($"I'm flying with a weight of: {Weight}");
    }
}

public interface ILizard
{
    int Weight { get; set; }
    void Crawl();
}

public class Lizard: ILizard
{
    public int Weight { get; set; }

    public void Crawl()
    {
        Console.WriteLine($"I'm crawling with a weight of: {Weight}");
    }
}

public class Dragon : IBird, ILizard
{
    private readonly Bird _bird = new();
    private readonly Lizard _lizard = new();
    private int weight;

    public int Weight
    {
        get => weight;
        set
        {
            weight = value;
            _bird.Weight = value;
            _lizard.Weight = value;
        }
    }

    public void Fly()
    {
        _bird.Fly();
    }

    public void Crawl()
    {
        _lizard.Crawl();
    }
}
