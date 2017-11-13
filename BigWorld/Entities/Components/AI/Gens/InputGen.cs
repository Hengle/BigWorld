using System;

namespace BigWorld.Entities.Components.AI.Gens
{
    public class InputGen : NeuronGen
    {
        public InputNeuron InputNeuron { get; set; }

        public InputGen(InputNeuron inputNeuron)
        {
            this.InputNeuron = inputNeuron;
        }

        public override Gen Copy()
        {
            return new InputGen(InputNeuron);
        }

        public override void Apply(NeuronList neuronList)
        {
            neuronList.Add(InputNeuron);
        }
    }
}