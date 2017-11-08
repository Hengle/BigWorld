using System;
using System.Threading;

namespace BigWorld.Entities.Components.AI.Gens
{
    [GenDefinition(2)]
    public class LinkGen : Gen
    {
        public int In { get; private set; }
        public int Out { get; private set; }
        public double Weight { get; private set; }
        public bool Enable { get; private set; }


        private static int linkGenCounter = 0;
        public readonly int LinkgenId;
        
        public LinkGen()
        {
            LinkgenId = Interlocked.Increment(ref linkGenCounter);
        }

        public LinkGen(int @in, int @out, double weight, bool enable)
            :this()
        {
            In = @in;
            Out = @out;
            Weight = weight;
            Enable = enable;
        }
        
        public LinkGen(int @in, int @out, double weight, bool enable, int linkgenId)
        {
            In = @in;
            Out = @out;
            Weight = weight;
            Enable = enable;

            LinkgenId = linkgenId;
        }
        
        public override Gen Copy()
        {
            return new LinkGen(In,Out,Weight,Enable,LinkgenId);
        }

        public override void CreateRandom(Random r, Genome genome)
        {
            In = r.Next(0, genome.NeuronGens.Count);
            Out = r.Next(0, genome.NeuronGens.Count);
            Weight = r.NextDouble() * 2 - 1;
            Enable = r.Next(0, 3) > 0;
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

        public override int GetHashCode()
        {
            return LinkgenId;
        }

        public override bool Equals(object obj)
        {
            if (obj is LinkGen linkGen)
            {
                return linkGen.LinkgenId == LinkgenId;
            }
            return false;
        }

        public void Mutate(Random r)
        {
            var applyMutate = r.Next(10) == 0;

            if (applyMutate)
            {
                Weight = Weight + (r.NextDouble() * 2 - 1) * 0.1  ;

                if (r.Next(0, 5) == 0)
                {
                    Enable = !Enable ;
                }
            }
        }
    }
}