# Structural Patterns

## Adapter

The concept of the Adapter pattern is simple:
you want to get the interface that you want from the interface that you are given.

### Motivation for Adapters

For a physical example think of an electrical plug and some device.
Different countries have different power (interface) requirements:

- Voltage (5V, 220V)
- Socket/plug type

We cannot modify the electrical devices to support every single
power (interface) requirement.
Thus, we use a special device (an adapter) to give us the interface
we require from the interface we have.

**Thus, in software, the Adapter is a construct which adapts an existing
interface X to conform to the required interface Y.**

One of the side-effects of the adapter pattern is that you have a lot
of temporary information, and work can duplicated.
You can deal with this by simply implementing caching or memoization,
to prevent the Adapter from re-calculating or re-adapting something
that has already been adapted.

### Generic Value Adapter

The Generic Value Adapter design pattern adapts a literal value
like 2 to a type. The reason for this is that when you have
generic classes you cannot pass literals as the generics,
you can only pass types.

So if you have a generic Vector class where one of the generic
parameters is the dimensions of the vector, you cannot specify
2, for example, even though that would be convenient.

So the Generic Value Adapter converts literals to types, so that
we can solve this problem.

### Summary

- Implementing an adapter is easy
- Determine the API that you have and the API that you need
- Create a component which aggregates (has a reference to) the adapted component
- Intermediate representations can pile up: use caching (memoization) and other optimizations
