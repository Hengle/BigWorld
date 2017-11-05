using System;

namespace BigWorld.Entities.Components.AI
{
    public class InputNeuron : Neuron
    {
        public void SetValue(float value)
        {
            if (value < -1 || value > 1)
                throw new ArgumentOutOfRangeException(nameof(value));

            Value = value;
        }
    }
}