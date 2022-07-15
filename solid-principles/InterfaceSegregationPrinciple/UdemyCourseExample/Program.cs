using System;

namespace InterfaceSegregationPrinciple
{
    public class Document
    {

    }

    #region Incorrect

    // We want to do different things with the document like printing, or scanning. But, there
    // is also a multifunction machine that can do printing and scanning. So we decide, INCORRECTLY,
    // to make one big interface for everything.

    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }

    // So you go and define a machine for this interface, and everything is great.

    public class MultifunctionPrinter : IMachine
    {
        public void Print(Document d)
        {

        }

        public void Scan(Document d)
        {

        }

        public void Fax(Document d)
        {

        }
    }

    // But what if you also want a standard printer? You can certainly implement the
    // interface and use the Print method, but what about the rest? Do you throw an
    // exception, do you do nothing, etc.? Whatever you do you'll have make really
    // good and obvious documentation to explain it, which is isn't scalable.

    public class SimplePrinter : IMachine
    {
        public void Print(Document d)
        {

        }

        public void Scan(Document d)
        {

        }

        public void Fax(Document d)
        {

        }
    }

    #endregion Incorrect

    #region Correct

    // This is where the interface segregation principle comes in. You don't pay
    // for things you don't need.

    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public interface IFax
    {
        void Fax(Document d);
    }

    public class Printer : IPrinter
    {
        public void Print(Document d)
        {

        }
    }

    public class PhotoCopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {

        }

        public void Scan(Document d)
        {

        }
    }

    // Or you can also define a higher level interface

    public interface IMultifunctionDevice : IPrinter, IScanner
    {
    }

    public class MultifunctionMachine : IMultifunctionDevice
    {
        public void Print(Document d)
        {

        }

        public void Scan(Document d)
        {

        }
    }

    #endregion Correct

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
