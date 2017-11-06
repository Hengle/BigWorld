using System;

namespace BigWorld.Entities.Components.AI
{
    public class HiddenNeuron : Neuron
    {
        public double Parameter { get; set; } = 1;
        
        protected override double InternGetValue(int run)
        {
            var linkValue = GetLinkValue(run);
            return 1 / (1 + Math.Exp(-Parameter * linkValue));
        }
    }
}