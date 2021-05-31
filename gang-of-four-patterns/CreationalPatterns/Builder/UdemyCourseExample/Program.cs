using System;
using System.Collections.Generic;
using System.Text;
using UdemyCourseExample;

namespace Builders
{
    class Program
    {
        static void Main(string[] args)
        {
            // Normal builder.
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "Hello").AddChild("li", "World");
            Console.WriteLine($"{builder}");

            // Builder inheritance.
            var p = Person2.New.Called("Bob").WorksAsA("Developer").Build();
            Console.WriteLine(p);

            // Functional builder.
            var p2 = new PersonBuilder2()
                .Called("John")
                .WorksAsA("Manager")
                .Build();
            Console.WriteLine(p2);

            var p3 = new PersonFunctionalBuilder()
                .Called("Sarah")
                .WorksAsA("Actuary")
                .Build();
            Console.WriteLine(p3);

            var facetedBuilder = new Person4Builder();
            Person4 p4 = facetedBuilder
                .Works
                    .At("Company A")
                    .AsA("Worker")
                    .Earning(123000)
                .Lives
                    .At("Street A")
                    .WithPostcode("456")
                    .In("City A");
                Console.WriteLine(p4);
        }
    }
}
