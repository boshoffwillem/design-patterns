# SOLID Principles

## Single Responsibility Principle

This principle specifies that any class should just have a single reason to change.
Putting it another way would be to call it **Separation of Concerns**: different classes
handle different, independent tasks/problems.

## Open-Closed Principle

Classes should be open for extension, but they should be closed for modification.
Using the Udemy course example, we have a ProductFilter class. The class should be open for extension, 
meaning  we should be able to add new filters. However, no one should be able to go into Product filter and edit any code there.

So how do we extent classes without changing their bodies? **Inheritance/Interfaces**.
To achieve this we implement a pattern. Not a gang of four pattern, but a enterprise pattern: **Specification pattern**.
The specification pattern basically dictates whether or not an object satisfies some particular criteria.

## Liskov Substitution Principle

The idea is that you should be able to substitute a base type for a sub-type.
The Liskov principle states that you should always be able to cast to your base type and everything should still be fine.
A simple way to achieve this is basically through polymorphism.

## Interface Segregation Principle

The idea is that when your interface gets to large, you segregate it into smaller interfaces, so that no one who
implements your interface has to implement functionality that they don't need.

## Dependency Inversion Principle

High level parts of the system should not depend on low level parts of the system directly; instead they should depend on some kind of distraction.
