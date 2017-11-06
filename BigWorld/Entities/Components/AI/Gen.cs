using System;

namespace BigWorld.Entities.Components.AI
{
    public abstract class Gen
    {        
        public virtual void CreateRandom(Random r,Genome genome)
        {
            
        }

        public abstract void Apply(NeuronList neuronList);
    }
}