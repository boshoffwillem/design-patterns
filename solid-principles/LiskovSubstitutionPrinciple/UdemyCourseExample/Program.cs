using System;

namespace LiskovSubstitutionPrinciple
{
    #region Incorrect

    public class RectangleIncorrect
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public RectangleIncorrect()
        {
            
        }

        public RectangleIncorrect(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class SquareIncorrect : RectangleIncorrect
    {
        public new int Width
        {
            set { base.Width = base.Height = value; }
        }
        public new int Height
        {
            set { base.Width = base.Height = value; }
        }
    }

    #endregion Incorrect

    #region Correct

    public class RectangleCorrect
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public RectangleCorrect()
        {
            
        }

        public RectangleCorrect(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class SquareCorrect : RectangleCorrect
    {
        public override int Width
        {
            set { base.Width = base.Height = value; }
        }
        public override int Height
        {
            set { base.Width = base.Height = value; }
        }
    }

    #endregion Correct

    class Program
    {
        static public int Area(RectangleIncorrect rectangle) => rectangle.Width * rectangle.Height;
        static public int Area(RectangleCorrect rectangle) => rectangle.Width * rectangle.Height;

        static void Main(string[] args)
        {
            #region The problem

            RectangleIncorrect rectangle = new RectangleIncorrect(2, 3);
            System.Console.WriteLine($"{rectangle} has area {Area(rectangle)}");

            SquareIncorrect square = new SquareIncorrect();
            square.Width = 4;
            System.Console.WriteLine($"{square} has area {Area(square)}");

            // Everything seems fine so far.
            // However, we should be able to define a Square as a Rectangle,
            // because of how inheritance works.

            // But, when we say
            RectangleIncorrect sq = new SquareIncorrect();
            sq.Width = 4;
            System.Console.WriteLine($"{sq} has area {Area(sq)}");
            // and then print the area, we get 0, because the rectangle has no height.

            // The Liskov principle states that you should always be able to cast to your
            // basetype and everything should still be fine.
            // The Square should still behave as a Square even when it's a Rectangle.

            #endregion The problem

            System.Console.WriteLine("\n");

            #region The solution

            // Basically you achieve this through Polymorphism. You define the basetypes
            // as virtual and then override them in the sub-classes.

            RectangleCorrect rectangle1 = new RectangleCorrect(2, 3);
            System.Console.WriteLine($"{rectangle1} has area {Area(rectangle1)}");
            SquareCorrect square1 = new SquareCorrect();
            square1.Width = 4;
            System.Console.WriteLine($"{square1} has area {Area(square1)}");
            RectangleCorrect sq1 = new SquareCorrect();
            sq1.Width = 4;
            System.Console.WriteLine($"{sq1} has area {Area(sq1)}");

            #endregion The solution
        }
    }
}
