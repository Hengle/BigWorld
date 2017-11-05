using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BigWorld.Entities.Components.AI
{
    public class Genome : List<Gen>
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

        private static int maxProp = 0;
        
        static Genome()
        {
            var ass = Assembly.GetExecutingAssembly();
            var genTypes = from type in ass.GetTypes()
                let attribute = type.GetCustomAttribute<GenDefinitionAttribute>()
                where typeof(Gen).IsAssignableFrom(type)
                select new {Type = type,Attribute = attribute };

            foreach (var type in genTypes)
            {
                var newMax = maxProp+(int)type.Attribute.Probabillity;
                gens.Add(type.Type,new InternGenDefinition(maxProp,newMax));

                maxProp = newMax;

            }
        }
        
        public static Genome CreateGenom(NeuronList basicList,int count)
        {
            Random r = new Random();
            var seed = r.Next();
            return CreateGenom(basicList,seed);
        }

        public static Genome CreateGenom(NeuronList basicList,int count,int seed)
        {
            Genome genome = new Genome();

            return genome;
        }
    }
}