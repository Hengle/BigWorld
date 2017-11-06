﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BigWorld.Entities.Components.AI.Gens;

namespace BigWorld.Entities.Components.AI
{
    public class Genome
    {
        private class InternGenDefinition
        {
            public readonly int StartProp;
            public readonly int EndProp;

            public InternGenDefinition(int startProp, int endProp)
            {
                StartProp = startProp;
                EndProp = endProp;
            }
        }
        
        private static readonly Dictionary<Type,InternGenDefinition> gens 
            = new Dictionary<Type, InternGenDefinition>();

        private static readonly int MaxProp = 0;
        
        public readonly List<LinkGen> LinkGens = new List<LinkGen>();
        public readonly List<NeuronGen> NeuronGens = new List<NeuronGen>();
        
        static Genome()
        {
            var ass = Assembly.GetExecutingAssembly();
            var genTypes = from type in ass.GetTypes()
                let attribute = type.GetCustomAttribute<GenDefinitionAttribute>()
                where attribute != null
                where typeof(Gen).IsAssignableFrom(type)
                select new {Type = type,Attribute = attribute };

            foreach (var type in genTypes)
            {
                var newMax = MaxProp+(int)type.Attribute.Probabillity;
                gens.Add(type.Type,new InternGenDefinition(MaxProp,newMax));

                MaxProp = newMax;

            }
        }
        
        
        
        public static Genome CreateGenom(NeuronList basicList,int count)
        {
            Random r = new Random();
            var seed = r.Next();
            return CreateGenom(basicList,count,seed);
        }

        public static Genome CreateGenom(NeuronList basicList,int count,int seed)
        {
            Genome genome = new Genome();

            genome.CreateRandomGens(count, seed);

            return genome;
        }

        public void CreateRandomGens(int count)
        {
            Random r = new Random();
            var seed = r.Next();
            CreateRandomGens(count,seed);
        }
        
        public void CreateRandomGens(int count, int seed)
        {
            Random r = new Random(seed);

            for (int i = 0; i < count; i++)
            {
                var genTypeNumber = r.Next(MaxProp);
                var genType = gens.First(g =>
                    genTypeNumber >= g.Value.StartProp && genTypeNumber < g.Value.EndProp);

                var gen = Activator.CreateInstance(genType.Key) as Gen;
                if (gen != null)
                {
                    gen.CreateRandom(r,this);
                    Add(gen);
                }
            }
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
    }
}