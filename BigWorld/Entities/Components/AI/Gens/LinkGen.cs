﻿using System;
using System.Threading;

namespace BigWorld.Entities.Components.AI.Gens
{
    public class LinkGen : Gen
    {
        private static int GlobalInovationNumber = 0;
        public readonly int InovationNumber;

        public int InNeuron { get; set; }
        public int OutNeuron { get; set; }
        
        public float Weight { get; set; }
        public bool Enable { get; set; }
        
        public LinkGen()
        {
            InovationNumber = Interlocked.Increment(ref GlobalInovationNumber);
        }

        public LinkGen(int inNeuron, int outNeuron, float weight, bool enable)
            :this()
        {
            InNeuron = inNeuron;
            OutNeuron = outNeuron;
            Weight = weight;
            Enable = enable;
        }


        private LinkGen(int inovationNumber)
        {
            InovationNumber = inovationNumber;
        }

        public static LinkGen CreateRandom(Random random,Tuple<int,int> pair)
        {
            var weight = random.NextDouble() * 2 - 1;
            return new LinkGen(pair.Item1,pair.Item2,(float)weight,true);
        }

        public override Gen Copy()
        {
            return new LinkGen(InovationNumber)
            {
                InNeuron = InNeuron,
                OutNeuron = OutNeuron,
                Weight = Weight,
                Enable = Enable,
            };
        }

        public override void Apply(NeuronList neuronList)
        {
            if (!Enable)
                return;
                
                
            var inputNeuron = neuronList[InNeuron];
            var outputneuron = neuronList[OutNeuron];
                
            outputneuron.Links.Add(new NeuronLink(inputNeuron,Weight));
        }

        public void Mutate(Random random)
        {
        }
    }
}