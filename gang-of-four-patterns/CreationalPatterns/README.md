# Creational Patterns

## Factories

### Motivation for factories

- Object creation becomes too convoluted
- The constructor is not descriptive enough (must be the name of the class)
- Can turn into "optional parameter hell"
- Factories fully creates an object, unlike builders which does it piece by piece
- Object creation is outsourced to
  - A separate method (**Factory Method**)
  - A separate class (**Factory**)
  - A hierarchy of factories (**Abstract Factory**)
- Factories are also a great way to achieve asynchronous object creation
- When using factories you should preferably make any constructors private, to prevent unintended object creation

**A factory is component whose sole responsibility is the wholesale creation of objects.**

### Factory Method

A simple static method that provides flexibility on how the containing class is created.

### Factory

It might be argued from the Single Responsibility Principle that the creation of an object should be a separate thing. Thus you simply move all your factory methods into a factory class.

The problem with this is that the constructor of you class has been made private, so that means the factory cannot access the constructor. Making the constructor public will solve the problem, but it's not ideal. We solve the problem by using an **Inner Factory**.

### Object Tracking and Bulk Replacemnt

You might ask what additional benefits are there to using factories. Should it always be static?
Can it contain member variables? etc.

One way to use factories is to track every object that has been created by the factory.
You can then also perform a bulk replacement of those references.

#### Inner Factory

In order for the factory to access private constructors, we move the factory class inside the class to be created. If we had Point and PointFactory, we would move PointFactory into Point and rename it to just Factory, since it's now obviously a factory of Points.

### Abstract Factory

In an abstract setting you are not returning the types that you are creating.
You are returning abstract classes or interfaces, which in turn creates families of objects.

## Builders

### Motivation for Builders

Some objects are simple and be created in a single constructor call.
Other objects require a lot of ceremony to create.
Having an object with 10 constructor arguments is not productive.

Instead, opt for piecewise construction.
A builder should provide a good API for constructing an object step-by-step.

- The sole purpose of a builder is to build up an object.
- You can either give the builder a constructor an explicitly create it,
or you can return the builder  from the related object through a static method.

### Builder

In the example we will build an HTML builder. HTML elements have an opening and closing tag,
as well as indentation for nested elements. Outputting a simple unordered list can be very tedious  
if not done with a builder. By writing a builder, we can easily output HTML without having to worry
about indentation and opening and closing tags. We implement the logic for indentation and tags once  
and we don't worry about it again.

### Fluent Builder

To achieve a fluent interface of the builder simply return the reference of your builder.
Then you can chain various calls to your builder.

### Faceted Builder

Sometimes object construction is too complex for one builder to handle, and so we need
to use multiple builders. You start by creating a general builder for the actual object.
But this builder won't do any building. It's a facade to other builders, and it keeps a reference
to the object that's being built.

### Stepwise Builder

There are times when you want a builder to perform a series of steps one after the other.

To achieve this you make use of the Interface Segregation principle. You create various interfaces
that each has a method whose return type is the interface containing the next method you need to
execute. You then put a factory method on the builder that returs the interface you need to
start with. This provides an interface chain that acts like a wizard, guiding you through the
creation process.

## Prototypes

The Prototype pattern is all about object copying.

### Motivation for Prototypes

- Complicated objects (e.g., cars) are not designed from scratch.
- New complicated objects (cars) are created by reiterating existing designs.
- A Prototype is an existing object that was partially of fully constructed.
- A Prototype is also an object that we can make a clone of and then customize it.
- To enable cloning the object must have deep copy support.
- The cloning must be convenient (e.g., via a Factory)

**A Prototype is a partially or fully initialized object that you copy and make use of**.

- To implement a prototype partially construct an object and store it somewhere
- Clone the prototype:
- Implement your own deep copy interface
- Serialize and de-serialize
- Customize the resulting instance

### Copy Constructors

A copy constructor is a C++ term. The idea is simple.
You add a constructor to your object (Prototype) that takes as a parameter the object (of the same type obviously)
that you want to deep copy. You then copy the internals that you want from that object into yourself.

The problem with this is that if you have an object that contains other objects, everyone of those objects needs
to have a copy constructor as well, otherwise their copy will be a shallow copy.

This approach will work, but the more complex that an object becomes the more tedious it will become to scale this approach.

### Explicit Deep Copy Interface

Another approach is to define and interface to deep copy an object.
Unfortunately we are still stuck with needing to have every object within the Prototype
implement the deep copy interface.

This approach is better, but this approach can still be very tedious.

### Prototype Inheritance

One of the problems with having a deep copy interface is what happens when you inherit from a class that
implements the deep copy interface. Like many solutions to inheritance problems you can get around it,
but it's going to be messy.

The reason for the messy part is that in order for Prototype Inheritance to work you need fully initializing constructors. So obviously as soon as you have any decent inheritance hierarchy this will get messy. If this you're fine with having fully initializing constructors all over the place then you don't have to do any magic and the deep copying will work.

If you want to avoid having fully initialing constructors then we have to do some magic by improving the interface and defining some extension methods.

### Copy Through Serialization

Making a deep copy of an object through serialization is a much easier and scalable way of doing it.
This is how the Prototype pattern is actually done in the real world: by serializing an object and then de-serializing it to have a deep copy of the object.

## Singleton

As the name implies, a singleton is used when you only want to allow a single instance of class during the lifetime of the program.

### Motivation for Singleton

For some components it makes sense to only have one of them in the system:

- Database repository
- Object factory

Other areas for possible singleton use also include where you have classes whose constructor call is expensive.

- Ideally we only want to call it once
- We provide everyone with the same instance
- We also want to prevent anyone from creating additional copies

**When creating Singletons we need to ensure that it's creation is lazy, and that it is thread safe.**

Basic Singleton pattern:

```csharp
class SingletonClass
{
    private static Lazy<SingletonExample> _instance = 
            new Lazy<SingletonExample>(() => new SingletonExample());

    private SingletonClass(){ }

    public static SingletonClass Instance => _instance.Value;
}
```

### Problems with the Singleton Pattern

#### Testability Issues

The problem with testing Singletons is that they have a hard reference to some instance.
For example if you have a Singleton to a object that accesses a database, when you use the
Singleton it will actually try and connect to the database. There is no way to fake the database
connection in unit tests, because you cannot fake the object reference of the Singleton.

#### Dependency Injection

The testability problems you get with a Singleton, as described above,
can be solved with Dependency Injection. By using a Dependency Container
you can change the Singleton class back to a normal class, and then register
the class as a singleton in your dependency container.

### Per-Thread Singleton

By using ```Lazy<T>``` in our Singleton pattern we guarantee thread-safe initialization of the Singleton.
But, there is something else you can do. If the scenario if appropriate you can also have a per-thread
Singleton. As the name specifies it means you'll a different Singleton on a different thread.

```csharp
public class PerThreadSingleton
{
    private static readonly ThreadLocal<PerThreadSingleton> _threadInstance = 
        new (() => new PerThreadSingleton());

    public int Id { get; set; }
    
    private PerThreadSingleton()
    {
        Id = Thread.CurrentThread.ManagedThreadId;
    }

    public static PerThreadSingleton Instance => _threadInstance.Value;
}

// Testing
public static void Main()
{
  var t1 = Task.Factory.StartNew(
  {
    () => Console.WriteLine("t1: " + PerThreadSingleton.Instance.Id);
  });

  var t2 = Task.Factory.StartNew(
  {
    () => Console.WriteLine("t2: " + PerThreadSingleton.Instance.Id);
    () => Console.WriteLine("t2: " + PerThreadSingleton.Instance.Id);
  });

  Task.WaitAll(t1, t2);
}

// Example output
// t1: 2
// t2: 4
// t2: 4
```

**It's worth noting this type of functionality is also available**
**by configuring your dependency injection container to have a Thread lifetime**,
instead of doing it manually.

### Monostate

There is a variation of the singleton pattern called **Monostate**.
The idea is that you have a class that is exposed in a non-static way, but it's state is static.

For example, let's say you the CEO of a company; a company can only have one CEO.
So you create the CEO as monostate.

```csharp
public class MonostateExample
{
    private static string _name;
    private static int _age;

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public int Age
    {
        get => _age;
        set => _age = value;
    }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
    }
}
```

This will allow you create multiple CEO objects, and the CEO's properties are exposed
as normal, but internally they all share the same state. This will prevent the scenario
where you accidentally have multiple CEO's with differing data. This pattern will force you
to have one CEO and when the CEO changes you'll have to update the data.

### Summary

- To make a thread-safe Singleton is easy. Simply construct a static ```Lazy<T>``` and return its value.
- Singletons are difficult to test.
- To overcome the testing difficulties, use the Dependency Inversion Principle. Make the Singleton depend
on an Interface or abstraction rather than a concrete instance.
- Try to define a Singleton using a DI container rather than explicitly making the class a Singleton.
