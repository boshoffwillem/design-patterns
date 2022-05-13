# Structural Patterns

## 1. Adapter

The concept of the Adapter pattern is simple:
you want to get the interface that you want from the interface that you are given.

### 1.1 Motivation for Adapters

For a physical example think of an electrical plug and some device.
Different countries have different power (interface) requirements:

- Voltage (5V, 220V)
- Socket/plug type

We cannot modify the electrical devices to support every single
power (interface) requirement.
Thus, we use a special device (an adapter) to give us the interface
we require from the interface we have.

**Thus, in software, the Adapter is a construct that adapts an existing
interface X to conform to the required interface Y.**

One of the side effects of the adapter pattern is that you have a lot
of temporary information, and work can duplicated.
You can deal with this by simply implementing caching or memoization,
to prevent the Adapter from re-calculating or re-adapting something
that has already been adapted.

### 1.2 Generic Value Adapter

The Generic Value Adapter design pattern adapts a literal value
like 2 to a type. The reason for this is that when you have
generic classes you cannot pass literals as the generics,
you can only pass types.

So if you have a generic Vector class where one of the generic
parameters is the dimensions of the vector, you cannot specify
2, for example, even though that would be convenient.

So the Generic Value Adapter converts literals to types, so that
we can solve this problem.

### 1.3 Summary

- Implementing an adapter is easy
- Determine the API that you have, and the API that you need
- Create a component that aggregates (has a reference to) the adapted component
- Intermediate representations can pile up: use caching (memoization) and other optimizations

## 2. Bridge

Connecting components through abstractions.

### 2.1 Motivation for Bridges

Bridge prevents a "Cartesian product" complexity explosion.

For example, You decide to make a ThreadScheduler. The ThreadScheduler can be

- Preemptive or cooperative
- Run on Windows or Unix

Now you have a 2x2 scenario:

- Preemptive Windows
- Cooperative Windows
- Preemptive Unix
- Cooperative Unix

So you end up with four classes effectively.
The Bridge pattern tries to avoid this entity/class explosion.

**So, the Bridge pattern is a mechanism that decouples an interface (hierarchy)
from an implementation (hierarchy).**

### 2.2 Summary

- A bridge is used to decouple an abstraction from an implementation.
- Both the abstraction and implementation can be hierarchies.
- A bridge is almost like a stronger form of encapsulation,
because it hides functionality by compartmentalizing it,
instead of doing through a simple inheritance hierarchy.

## 3. Composite

A mechanism for treating individual (scalar) objects
and compositions of objects in a uniform manner.

### 3.1 Motivation for Composites

- Objects uses other object's fields/properties/members through inheritance or composition
- Composition lets us make compound objects
  - Like a mathematical expression composed of simple expressions
  - A grouping of shapes that consists of several shapes
- Composite design pattern is used to treat both single (scalar)
and composite objects uniformly
    - I.e., `Foo` and `Collection<Foo>` have common APIs
    
### 3.2 Summary

- Objects can use all other objects via inheritance/composition
- Some composed and singular objects need similar/identical behaviors
- Composite design pattern lets us treat both types of objects uniformly
- A single object can masquerade as a collection with `yield return this;`.

## 4. Decorator

Adding behavior to classes without altering the classes themselves.

### 4.1 Motivation for Decorator

- Want to augment an object with additional functionality
- Do not want to rewrite or alter existing code -- Open-Close Principle
- Want to keep new functionality separate -- Single Responsibility Principle
- Need to be able to interact with existing structures, so our decorated object
needs to have certain traits of the original object
- Two options:
  - Inherit from object if possible; some objects are sealed
  - Build a Decorator, which simply references the decorated object(s)
  and then provides additional behavior on top of those object(s).
  Remember because we don't use inheritance in this case we'll have
  to replicate the API and proxy the calls
  
The Decorator pattern facilitates the addition of behaviors to
individual objects without inheriting from them.
    
### 4.2 Summary

- A decorator keeps the reference to the decorated object(s)
- May or may not proxy over the calls
- A **static** variation does exists
    - `<T<Foo>>`
    - Very limited due to inability to inherit from type parameters

## 5. Facade

Exposing several components through a single interface.

### 5.1 Motivation for Facade

Let's use a house as an example.

- A house has to balance complexity and presentation/useability
- The typical home:
    - Has many subsystems (electrical, sanitation, etc.)
    - Complex internal structure (e.g., floor layers)
    - End user is not exposed to internals
- Same with software!
    - Many systems working to provide flexibility, but...
    - API consumers want it to just work

The Facade design pattern is all about providing are simple,
easy to understand interface over a large and sophisticated
body of code.
    
### 5.2 Summary

- Build a facade to provide a simplified API over a set
of classes
- May wish to (optionally) expose internals through the facade
- May allow users to 'escalate' to use more complex APIs
if they need to

## 6. Flyweight

The flyweight helps us achieve a single goal, and that goal is
space optimization. By space optimization we mean the amount of
memory that our applications take up.

### 6.1 Motivation for Flyweight

The goal of the Flyweight is to:
- Avoid redundancy when storing data
- E.g., MMORPG
    - Plenty of users with identical first/last names
    - No sense in storing same first/last name over and over again
    - Store a list of names and pointers to them
- .Net actually tries to do this with string interning, so an identical string is stored only once
- Another example is that you're writing bold or italic text in the console
    - Don't want each character to have a formatting character
    - Operate on ranges (e.g., line number, start/end postitions

The Flyweight pattern is a space optimization technique that lets us use less
memory by storing externally the data associated with similar objects. The key here
is that it avoids duplication.

### 6.2 Summary

- Store common data externally
