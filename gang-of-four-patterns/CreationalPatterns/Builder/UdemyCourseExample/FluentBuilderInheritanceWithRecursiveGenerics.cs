namespace UdemyCourseExample
{
     // Let's take a look a the scenario when builders inherit from other builders.
    // Usually this is not a problem, but when your builders are fluent then this becomes a problem.
    // There is no easy way to mitigate the inheritance of fluent interfaces. The most well-known approach
    // to doing this is to use recursive generics.

    // Here is the problem:

    // We have a class that contains employment information about people.
    public class Person
    {
        public string Name;

        public string Position;

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    // Now we want to build this:
    public class PersonInfoBuilder
    {
        protected Person person = new();

        public PersonInfoBuilder Called(string name)
        {
            person.Name = name;
            return this;
        }
    }

    // This builder will work fine as long as we don't inherit from it -- but we are.
    // Let's imagine there is another business requirement: we need another builder,
    // that must also have all the functionality of the existing one.
    // We follow the Open-Close principle, so we don't modify the existing builder,
    // but we extend (inherit) it.

    public class PersonJobBuilder : PersonInfoBuilder
    {
        public PersonJobBuilder WorksAsA(string position)
        {
            person.Position = position;
            return this;
        }
    }

    // No this seems to be all well, if you try this out there is a problem.
    // We want to say jobBuilder.Called("Bob").WorksAsA("Developer");
    // But we can't. The reason is that Called() returns a PersonInfoBuilder,
    // which doesn't know anything about WorksAsA().
    // The problem is that anytime you return a base type, you loose all the
    // functionality of any sub types.

    // So we need to return something more sophisticated. The question is how,
    // can a derived type propagate it's information to the base class? We want need
    // to be able to this recursively and infinitely.
    // The answer is recursive generics.

    public abstract class PersonBuilder
    {
        protected Person2 Person = new();

        public Person2 Build() => Person;
    }

    // You might say "this makes no sense". We have an argument, SELF,
    // and we're expecting that argument to inherit from the current object.
    // Basically we're only allowing one scenario: where SELF actually refers
    // to an object inheriting from this object.
    public class PersonInfoBuilder2<SELF> : PersonBuilder
        where SELF : PersonInfoBuilder2<SELF>
    {
        public SELF Called(string name)
        {
            Person.Name = name;
            return (SELF) this;
        }
    }

    public class PersonJobBuilder2<SELF> 
        : PersonInfoBuilder2<PersonJobBuilder2<SELF>>
        where SELF : PersonJobBuilder2<SELF>
    {
        public SELF WorksAsA(string position)
        {
            Person.Position = position;
            return (SELF) this;
        }
    }

    public class Person2
    {
        public string Name;

        public string Position;

        public class Builder : PersonJobBuilder2<Builder>
        {

        }

        public static Builder New => new();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }
}