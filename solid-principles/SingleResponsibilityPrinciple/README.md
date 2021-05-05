# SOLID Principles

## Single Responsibility Principle

This principle specifies that any class should just have a single reason to change.
Putting it another way would be to call it **Separation of Concerns**.

## Open-Closed Principle

Classes should be open for extension, but they should be closed for modification.
Using the Udemy course example, we have a ProductFilter class. The class should be open for extension, 
meaning  we should be able to add new filters. However, no one should be able to go into Product filter and edit any code there.

So how do we extent classes without changing their bodies? **Inheritance/Interfaces**.
To achieve this we implement a pattern. Not a gang of four pattern, but a enterprise pattern: **Specification pattern**.
The specification pattern basically dictates whether or not an object satisfies some particular criteria.
