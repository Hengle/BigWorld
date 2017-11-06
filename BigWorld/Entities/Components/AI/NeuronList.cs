using System.Collections.Generic;

namespace BigWorld.Entities.Components.AI
{
    public class NeuronList
    {
        public int Run { get; private set; }
        
        private readonly List<Neuron> neurons = new List<Neuron>();
        
        public Neuron this[int index] => neurons[index];
        
        public T CreateNeuron<T>()
            where T: Neuron,new()
        {
            T neuron = new T();
            Add(neuron);
            return neuron;
        }

        public void Add(Neuron neuron)
        {
            neurons.Add(neuron);
        }
        
        public void CreateNeuronsFromGenome(Genome genom)
        {
            foreach (var gen in genom.NeuronGens)
            {
                gen.Apply(this);
            }
            foreach (var gen in genom.LinkGens)
            {
                gen.Apply(this);
            }
        }
        
        public void Update()
        {
            var run = ++Run;
            foreach (var neuron in neurons)
            {
                neuron.GetValue(run);
            }
            
        }


        
    }
}