using System;

namespace BigWorld.Entities.Components.AI.Gens
{
    [GenDefinition(2)]
    public class LinkGen : Gen
    {
        public int In { get; private set; }
        public int Out { get; private set; }
        public double Weight { get; private set; }
        public bool Enable { get; private set; }

        public LinkGen()
        {
            
        }

        public LinkGen(int @in, int @out, double weight, bool enable)
        {
            In = @in;
            Out = @out;
            Weight = weight;
            Enable = enable;
        }

        public override void CreateRandom(Random r, Genome genome)
        {
            In = r.Next(0, genome.NeuronGens.Count);
            Out = r.Next(0, genome.NeuronGens.Count);
            Weight = r.NextDouble() * 2 - 1;
            Enable = r.Next(0, 2) == 1;
        }

        public override void Apply(NeuronList neuronList)
        {
            if (!Enable)
                return;
            
            var inputNeuron = neuronList[In];
            var outputNeuron = neuronList[Out];
            
            NeuronLink link = new NeuronLink(inputNeuron,Weight);
            
            outputNeuron.Links.Add(link);
        }
    }
}