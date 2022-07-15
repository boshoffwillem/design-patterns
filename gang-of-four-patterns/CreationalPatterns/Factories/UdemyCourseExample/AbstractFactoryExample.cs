using System;
using System.Collections.Generic;

namespace Factories
{
    #region Abstract Factory Example

    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This is tea.");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This is coffee.");
        }
    }

    // We do no want to expose Tea or Coffee, but only IHotDrink.
    // But we want different factories for tea and coffee.

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Making {amount} ml tea in a factory.");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Making {amount} ml coffee in a factory.");
            return new Coffee();
        }
    }

    // Notice we also don't expose the factories.
    // We now make another class that exposes all the functionlaity.

    public class HotDrinkMachine
    {
        /*
        public enum AvailableDrink
        {
            Coffee,
            Tea
        }
        private Dictionary<AvailableDrink, IHotDrinkFactory> factories = new();
        public HotDrinkMachine()
        {
            foreach (var drink in Enum.GetValues(typeof(AvailableDrink)))
            {
                var factory = (IHotDrinkFactory)Activator.CreateInstance(
                    Type.GetType("Factories. " + Enum.GetName(typeof(AvailableDrink), drink) + "Factory")
                );
                factories.Add(drink, factory);
            }
        }
        public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        {
            return factories[drink].Prepare(amount);
        }
        */

        #region Better solution

        // The problem with the approach so far is that by using an enum,
        // we are violating the Open-Close Principle.
        // To solve this we use a more interactive approach -- Reflection.

        // We can find every type in the assembly that implements IHotDrinkFactory.
        // (The best solution to doing this is probably through dependency injection,
        // but reflection will do for now.)

        private List<Tuple<string, IHotDrinkFactory>> factories = new();

        public HotDrinkMachine()
        {
            foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t) &&
                    !t.IsInterface)
                {
                    factories.Add(Tuple.Create(
                        t.Name.Replace("Factory", string.Empty),
                        (IHotDrinkFactory)Activator.CreateInstance(t)
                    ));
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Available drinks:");

            for (int i = 0; i < factories.Count; i++)
            {
                var factory = factories[i];
                Console.WriteLine($"{i}: {factory.Item1}");
            }

            while (true)
            {
                string s;
                if ((s = Console.ReadLine()) is not null &&
                    int.TryParse(s, out int i) &&
                    i >= 0 &&
                    i < factories.Count)
                {
                    Console.Write("Specify amount: ");
                    s = Console.ReadLine();

                    if (s is not null &&
                        int.TryParse(s, out int amount) &&
                        amount > 0)
                    {
                        return factories[i].Item2.Prepare(amount);
                    }
                }

                Console.WriteLine("Incorrect input, try again!");
            }

            return null;
        }

        #endregion
    }

    #endregion
}

