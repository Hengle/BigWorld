using System;

namespace BigWorld.Entities.Components.AI
{
    public class InputNeuron : Neuron
    {
        public double Value { get; private set; }
        
        public void SetValue(double value)
        {
            if (value < -1 || value > 1)
                throw new ArgumentOutOfRangeException(nameof(value));

            Value = value;
        }

        protected override double InternGetValue(int run)
        {
            return Value;
        }

    }
}