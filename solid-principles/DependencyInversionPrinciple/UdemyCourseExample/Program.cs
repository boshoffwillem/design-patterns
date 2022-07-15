using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInversionPrinciple
{
    public enum Relationship
    {
        Parent,
        Child
    }

    public class Person
    {
        public string Name { get; set; }
    }

    #region Incorrect Way

    // Low-Level
    public class RelationshipsIncorrect
    {
        private List<(Person, Relationship, Person)> _relations = new();

        public List<(Person, Relationship, Person)> Relations => _relations;

        public void AddParentAndChild(Person parent, Person child)
        {
            _relations.Add((parent, Relationship.Parent, child));
            _relations.Add((child, Relationship.Child, parent));
        }
    }

    public class ResearchIncorrect
    {
        public ResearchIncorrect(RelationshipsIncorrect relationships)
        {
            var relations = relationships.Relations;

            foreach (var r in relations.Where(
                x => x.Item1.Name == "John" &&
                x.Item2 == Relationship.Parent
            ))
            {
                System.Console.WriteLine($"John has a child called: {r.Item3.Name}");
            }
        }
    }

    #endregion

    // This scenario works fine and seems acceptable.
    // The problem with this scenario and the reason why the Dependency Inversion Principle exists,
    // is that we're accessing a very low-level part of the Relationships class: we're accessing it's data store, 
    // and we're accessing it through a specifically designed property to expose that data.
    // What this means in practice is that Relationships cannot change how it stores the relationships.
    // If we realize that it's much better to store the relationships using a Dictionary rather than a Tuple,
    // we can't change it without affecting higher-level parts.

    // A better design would be to abstract they way the Relationships are accessed through an interface.

    #region Correct Way

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOff(string name);
    }

    public class RelationshipsCorrect : IRelationshipBrowser
    {
        private List<(Person, Relationship, Person)> _relations = new();

        public void AddParentAndChild(Person parent, Person child)
        {
            _relations.Add((parent, Relationship.Parent, child));
            _relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOff(string name)
        {
            return _relations
            .Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent)
            .Select(x => x.Item3);
        }
    }

     public class ResearchCorrect
    {
        public ResearchCorrect(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOff("John"))
            {
                System.Console.WriteLine($"John has a child called: {p.Name}");
            }
        }
    }

    #endregion

    class Program
    {
         static void Main(string[] args)
        {
            var parent = new Person{Name = "John"};
            var child1 = new Person{Name = "Chris"};
            var child2 = new Person{Name = "Mary"};
            var r1 = new RelationshipsIncorrect();
            r1.AddParentAndChild(parent, child1);
            r1.AddParentAndChild(parent, child2);
            new ResearchIncorrect(r1);

            System.Console.WriteLine("\n");
            System.Console.WriteLine("\n");

            var r2 = new RelationshipsCorrect();
            r2.AddParentAndChild(parent, child1);
            r2.AddParentAndChild(parent, child2);
            new ResearchCorrect(r2);
        }
    }
}
