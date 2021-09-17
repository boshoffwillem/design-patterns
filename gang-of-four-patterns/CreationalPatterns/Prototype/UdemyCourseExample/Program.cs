using System;

namespace UdemyCourseExample
{
    class Program
    {
        static void Main()
        {
            #region Copy Constructor

            Console.WriteLine("Copy Constructor");

            var john1 = new Person1(new []{"John", "Smith"}, 
                new Address1("London Road", 123));
            Console.WriteLine(john1);

            var jane1 = new Person1(john1);
            jane1.Names[0] = "Jane";
            jane1.Address.HouseNumber = 321;
            Console.WriteLine(jane1);

            #endregion

            #region Deep Copy Interface

            Console.WriteLine("\nDeep Copy Interface");

            var john2 = new Person2(new []{"John", "Smith"}, 
                new Address2("London Road", 123));
            Console.WriteLine(john2);

            var jane2 = john2.DeepCopy();
            jane2.Names[0] = "Jane";
            jane2.Address.HouseNumber = 321;
            Console.WriteLine(jane2);

            #endregion

            #region Prototype Inheritance

            Console.WriteLine("\nPrototype Inheritance");

            var john3 = new Employee(new []{"John", "Smith"}, 
                new Address3("London Road", 123), 100000);
            Console.WriteLine(john3);

            var jane3 = john3.DeepCopy();
            jane3.Names[0] = "Jane";
            jane3.Address.HouseNumber = 321;
            jane3.Salary = 50000;
            Console.WriteLine(jane3);

            #endregion

            #region Deep Copy Through Serialization

            Console.WriteLine("\nDeep Copy Through Serialization");

            var john4 = new Person4(new []{"John", "Smith"},
                new Address4("London Road", 123));
            Console.WriteLine(john4);

            var jim = john4.SerializationDeepCopyXml();
            jim.Names[0] = "Jim";
            jim.Address.HouseNumber = 78;
            Console.WriteLine(jim);

            #endregion
        }
    }
}
