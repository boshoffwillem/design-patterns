using System.Collections.Generic;
using System;
using System.Linq;

namespace UdemyCourseExample
{
    public class Person3
    {
        public string Name;

        public string Position;

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }
    
    #region Solution

    public sealed class PersonBuilder2
    {
        private readonly List<Func<Person3, Person3>> _actions = new();

        private PersonBuilder2 AddAction(Action<Person3> action)
        {
            _actions.Add(p => 
            {
                action(p);
                return p;
            });
            return this;
        }

        public PersonBuilder2 Do(Action<Person3> action) => AddAction(action);

        public PersonBuilder2 Called(string name) => Do(p => p.Name = name);

        public Person3 Build() => _actions.Aggregate(new Person3(), (p, f) => f(p));
    }

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder2 WorksAsA(this PersonBuilder2 builder2, string position)
            => builder2.Do(p => p.Position = position);
    }

    #endregion


    #region Better Solution

     // All of the PersonBuilder2 code is reusable and can be generalized.
    public abstract class FunctionalBuilder<TSubject, TSelf>
        where TSelf : FunctionalBuilder<TSubject, TSelf>
        where TSubject : new()
    {
        private readonly List<Func<Person3, Person3>> _actions = new();

        private TSelf AddAction(Action<Person3> action)
        {
            _actions.Add(p => 
            {
                action(p);
                return p;
            });
            return (TSelf) this;
        }

        public TSelf Do(Action<Person3> action) => AddAction(action);

        public TSelf Called(string name) => Do(p => p.Name = name);

        public Person3 Build() => _actions.Aggregate(new Person3(), (p, f) => f(p));
    }

    public sealed class PersonFunctionalBuilder : FunctionalBuilder<Person3, PersonFunctionalBuilder>
    {
        public PersonFunctionalBuilder Called(string name) => Do(p => p.Name = name);
    }

    public static class PersonFunctionalBuilderExtensions
    {
        public static PersonFunctionalBuilder WorksAsA(this PersonFunctionalBuilder builder2, string position)
            => builder2.Do(p => p.Position = position);
    }

    #endregion
}