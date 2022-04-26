using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Composite
{
    public class Neuron : IEnumerable<Neuron>
    {
        public float Value;
        public List<Neuron> In, Out;

        // This is replaced by the extension method.
        // public void ConnectTo(Neuron other)
        // {
        //     Out.Add(other);
        //     other.In.Add(this);
        // }

        public IEnumerator<Neuron> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// Adding this NeuronLayer creates a problem.
    /// If we still want to follow the Composite pattern,
    /// we have the following requirements:
    /// 1. Connect a Neuron to a Neuron
    /// 2. Connect a Neuron to a NeuronLayer
    /// 3. Connect a NeuronLayer to a Neuron
    /// 4. Connect a NeuronLayer to a NeuronLayer.
    ///
    /// We currently only cover the first scenario.
    ///
    /// No we could add three more methods to cover the
    /// remaining scenarios, but this is ineficient.
    /// What if we later add a NeuronRing that implements
    /// some other data structure?
    ///
    /// This is why we have the composite pattern. We can
    /// have a scenario where we only have a single method
    /// to cover all these requirements.
    ///
    /// We do this by using an extension method.
    /// But an extension method on what? Neuron and NeuronLayer
    /// do not share a common base class...
    ///
    /// The trick is treat both Neuron and NeuronLayer as a
    /// collection of Neurons.
    ///
    /// We do this by having Neuron and NeuronLayer, and any future
    /// structure implement IEneumerable. NeuronLayer already does
    /// this via Collection.
    ///
    /// The problem with Neuron implementing IEnumerable is that
    /// Neuron is a scalar value and we want to treat it as a collection.
    /// The only way to this is yield return.
    /// </summary>
    public class NeuronLayer : Collection<Neuron>
    {
    }

    /// <summary>
    /// So we make an extension method on IEnumerable.
    /// </summary>
    public static class ExtensionMethods
    {
        public static void ConnectTo(this IEnumerable<Neuron> self,
            IEnumerable<Neuron> other)
        {
            if (ReferenceEquals(self, other)) return;

            foreach (var from in self)
            {
                foreach (var to in other)
                {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
            }
        }
    }
}
