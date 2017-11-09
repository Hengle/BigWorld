using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BigWorld.Entities.Components.AI.Gens;

namespace BigWorld.Entities.Components.AI
{
    public class Genome
    {


        public int Generation { get; private set; }

        
        public readonly List<LinkGen> LinkGens = new List<LinkGen>();
        public readonly List<NeuronGen> NeuronGens = new List<NeuronGen>();

        private int seed;
        private readonly Random random;
        

        public Genome(int seed)
        {
            this.seed = seed;
            random = new Random(seed);
            Generation = 1;
        }

        public Genome CopyGenome()
        {
            var newSeed = random.Next();
            
            Genome newGenome = new Genome(newSeed)
            {
                Generation = Generation + 1,
            };

            foreach (var gen in LinkGens)
            {
                newGenome.Add(gen.Copy());
            }

            foreach (var gen in NeuronGens)
            {
                newGenome.Add(gen.Copy());
            }
            
            return newGenome;
        }
        
        

        public void Mutate()
        {
            var mutateApply = (random.Next(1)) == 0;

            if (mutateApply)
            {

                var linkMutate = (random.Next(2)) == 0;
                
                if (linkMutate)
                {
                    CreateRandomLinkGen();
                }
                else
                {
                    CreateRandomNeuronGen();
                }
            }
        }
        
        public void CreateRandomLinkGen()
        {
            var links = GetLink();

            var exist = LinkGens.Any(i => i.InNeuron == links.Item1 && i.OutNeuron == links.Item2);

            if (exist)
                return;

            var linkgen = LinkGen.CreateRandom(random,links);
            
            Add(linkgen);
        }

        public void CreateRandomNeuronGen()
        {
            var links = GetLink();

            var link = LinkGens.FirstOrDefault(i => i.InNeuron == links.Item1 && i.OutNeuron == links.Item2);

            if (link != null)
            {
                link.Enable = false;
                
                var gen = new HiddenNeuronGen();
                Add(gen);
                var position = NeuronGens.Count -1;
                
                var linkgen1 = LinkGen.CreateRandom(random,new Tuple<int, int>(links.Item1,position));
                var linkgen2 = LinkGen.CreateRandom(random,new Tuple<int, int>(position,links.Item2));
                
                Add(linkgen1);
                Add(linkgen2);
            }
        }

        private Tuple<int, int> GetLink()
        {
            int f = 0;
            int s = 0;

            bool ok = false;
            int i = 0;
            
            do
            {
                f = random.Next(NeuronGens.Count);
                s = random.Next(NeuronGens.Count);

                if (f == s)
                    continue;

                ok = true;

            } while (i++ < 20 && !ok);

            if (f == s)
                throw new Exception();
            
            return new Tuple<int, int>(f,s);
        }
        

        public void Add(Gen gen)
        {
            switch (gen)
            {
                case LinkGen linkGen:
                    LinkGens.Add(linkGen);
                    break;
                case NeuronGen neuronGen:
                    NeuronGens.Add(neuronGen);
                    break;
            }
        }


        public Genome Combine(Genome secoundGenome)
        {
            
            var newSeed = random.Next();
            
            Genome newGenome = new Genome(newSeed)
            {
                Generation = (Generation > secoundGenome.Generation ? Generation : secoundGenome.Generation) +1,
            };

            var neuronGens = NeuronGens.Count > secoundGenome.NeuronGens.Count ? NeuronGens : secoundGenome.NeuronGens;
            

            foreach (var gen in neuronGens)
            {
                newGenome.Add(gen.Copy());
            }

            var linkgens = LinkGens.Union(secoundGenome.LinkGens);
            
            foreach (var gen in linkgens)
            {
                var copyGen = (LinkGen)gen.Copy();
                copyGen.Mutate(random);
                
                newGenome.Add(copyGen);
            }
            
            return newGenome;
        }
    }
}