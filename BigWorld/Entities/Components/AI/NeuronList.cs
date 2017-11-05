using System.Collections.Generic;

namespace BigWorld.Entities.Components.AI
{
    public class NeuronList
    {
        private readonly List<Neuron> neurons = new List<Neuron>();

        public T CreateNeuron<T>()
            where T: Neuron,new()
        {
            T neuron = new T();
            neurons.Add(neuron);
            return neuron;
        }

        public void CreateNeuronsFromGenome(Genome gen)
        {
            
        }
        
        public void Update()
        {
        }
    }
}