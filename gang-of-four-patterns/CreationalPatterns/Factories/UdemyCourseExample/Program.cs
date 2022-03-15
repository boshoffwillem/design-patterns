using System;

namespace Factories
{
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
