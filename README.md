# design-patterns

This is a repo to document design patterns that I learned from a Udemy course (<https://www.udemy.com/share/1013nUAEYacF9SR3Q=/>).

The course is on the design patterns designed in the Gang of Four book.
This repository also contains any other patterns that I've come to learn.

## Recursive Generics Pattern

This pattern is defined first, becuase many of the other examples use this.
Recursive generics is used when you have a decent inheritance hierarchy
and you need to be able to propagate the most derived class to either
the base class or any other parent class.
Excellent article: (<https://vyazelenko.com/2012/03/02/recursive-generics-to-the-rescue/>)

Recursive generics is a cleaner, more elegant solution,
than using Co-variant return types.

Co-variant return types is to permit the override of a method to return a more
derived return type than the method it overrides, and similarly to permit the
override of a read-only property to return a more derived return type.
Callers of the method or property would statically receive the more refined
return type from an invocation, and overrides appearing in more derived types
would be required to provide a return type at least as specific as that
appearing in overrides in its base types.

The problem that both **Recursive generics** and **Co-variant return types**
are trying to solve is the scenario where you have a base class method that returns
the base type. Then when you call that method from derived classes you actually want
that method to return the derived type instead of the base type.

A classic example is that you define a factory method in the base class
that returns the base class. But when you call the factory method from derived
classes it should return the derived class.

## SOLID Principles

- Single Responsibility Principle
- Open-Closed Principle
- Liskov Substitution Principle
- Interface Segregation Principle
- Dependency Inversion Principle

## Gang of Four Patterns

### Creational Patterns

Deals with the creation (construction) of objects.
Explicit (constructor) creation vs. implicit (dependency injection, etc.) creation.
Wholesale (single statement) creation vs. piecewise (step-by-step) creation.

- Factories
- Builders
- Prototypes
- Singleton

### Structural Patterns

Concerned with the structure of classes (e.g. class members).
Many patterns are wrappers that mimic the underlying class' interface.
Stress the importance of good API design.

- Adapter
- Bridge
- Composite
- Decorator
- Facade

### Behavioral Patterns

The patterns are all different; no central theme.

## Patterns of Enterprize Architecture

### Transaction Script

This pattern is a very simple way of organizing domain logic.
It's just a single procedure on top of the database, and inside
that procedure is where your domain logic is located.

Since all the domain logic is wrapped up in that procedure,
it's best used for simple situations. If you domain logic becomes
to rich, you might have to refactor to something like the Domain Model.

Transaction Scripts are kind of an anti-object-oriented approach -- which
is not a problem; it depends on the use case.

In the long run this pattern is probably not sustainable -- it doesn't scale.
Your probably better of just starting with the Domain Model pattern.

### Domain Model

The Domain Model is more of an object-oriented approach to domain modeling.
The objects capture all the domain logic and since we're working with
objects it really stresses design patterns. Which will mean you more likely
to end up with a nice loosely-coupled architecture.
