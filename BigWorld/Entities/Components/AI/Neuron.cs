using System.Collections.Generic;

namespace BigWorld.Entities.Components.AI
{
    public abstract class Neuron
    {
        public List<NeuronLink> Links { get;  } = new List<NeuronLink>();

        private double oldValue;
        private int oldRun;

        private int currentRun;
        private double? currentValue;
        
        public double GetValue(int run)
        {
            if (currentRun == run)
            {
                if (currentValue.HasValue)
                    return currentValue.Value;

                return oldValue;
            }

            oldRun = currentRun;
            currentRun = run;
            oldValue = currentValue ?? 0;
            
            currentValue = null;
            currentValue = InternGetValue(run);

            return currentValue.Value;
        }
        
        protected double GetLinkValue(int run)
        {
            double value = 0;
            
            foreach (var link in Links)
            {
                value += link.GetValue(run);
            }

            return value;
        }
        
        protected abstract double InternGetValue(int run);
    }
}