using System;
using System.Collections.Generic;

namespace Factories
{
    #region Factory Example

    public class Point
    {
        private double x, y;

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        // This is a Factory, and specifically an Inner Factory.
        public static class Factory
        {
            // This is a Factory Method.
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        // This is a Factory Method.
        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
        }
    }

    #endregion

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

    class Program
    {
        static void Main(string[] args)
        {
            var cartesianPoint = Point.Factory.NewCartesianPoint(2, 3);
            var polarPoint = Point.Factory.NewPolarPoint(4, Math.PI / 2);


            //var machine = new HotDrinkMachine();
            //var tea = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);
            //tea.Consume();
            //var coffee = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 100);
            //coffee.Consume();

            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink();
            drink.Consume();

            var trackingFactory = new TrackingThemeFactory();
            var theme1 = trackingFactory.CreateTheme(false);
            var theme2 = trackingFactory.CreateTheme(true);
            Console.WriteLine(trackingFactory.Info);

            var replaceableFactory = new ReplaceableThemeFactory();
            var magicTheme1 = replaceableFactory.CreateTheme(true);
            var magicTheme2 = replaceableFactory.CreateTheme(true);
            Console.WriteLine(magicTheme1.Value.BgrColor);
            Console.WriteLine(magicTheme2.Value.BgrColor);
            replaceableFactory.ReplaceThemes(false);
            Console.WriteLine(magicTheme1.Value.BgrColor);
            Console.WriteLine(magicTheme2.Value.BgrColor);
        }
    }
}
