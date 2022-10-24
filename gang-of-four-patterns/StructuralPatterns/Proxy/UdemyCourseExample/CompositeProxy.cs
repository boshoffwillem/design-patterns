/// <summary>
/// There is a pattern that combines the composite pattern
/// and the Proxy to create a pattern or construct known as the
/// Array of Structures/Structures of Array (SoA/AoS) paradox.
///
/// This pattern is especially used in game development, but is
/// also applicable to any scenario where large volumes of structured
/// data is processed.
/// </summary>
public class CompositeProxy {}

/// <summary>
/// Let's take the game scenario.
/// Suppose we have a game and the game has
/// a large number of creatures which participate in the game.
/// </summary>
public class Creature
{
    public byte Age;
    public int X, Y;
}

public class Creatures
{
    private readonly int _size;
    private byte[] _age;
    private int[] _x, _y;

    public Creatures(int size)
    {
        _size = size;
        _age = new byte[_size];
        _x = new int[_size];
        _y = new int[_size];
    }

    public struct CreatureProxy
    {
        private readonly Creatures _creatures;
        private readonly int _index;

        public CreatureProxy(Creatures creatures, int index)
        {
            _creatures = creatures;
            _index = index;
        }

        public ref byte Age => ref _creatures._age[_index];
        public ref int X => ref _creatures._x[_index];
        public ref int Y => ref _creatures._y[_index];
    }

    public IEnumerator<CreatureProxy> GetEnumerator()
    {
        for (var pos = 0; pos < _size; pos++)
            yield return new CreatureProxy(this, pos);
    }
}

public class Game
{
    public Game()
    {
        var creatures = new Creature[100];

        foreach (var c in creatures)
        {
            c.X++;
        }

        // Modern CPUs really like structured data.
        // Doing it this way would result in a memory layout like
        // Age X Y Age X Y Age X Y
        // which is not efficient.
        //
        // An efficient layout would be
        // Age Age Age
        // X    X   X
        // Y    Y   Y

        var creatures2 = new Creatures(100);
        foreach (Creatures.CreatureProxy c in creatures2)
        {
            c.X++;
        }
    }
}
