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
