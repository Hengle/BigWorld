using System;

namespace BigWorld.Entities.Components.AI.Gens
{
    public class InputGen : NeuronGen
    {
        private readonly InputNeuron inputNeuron;

        public InputGen(InputNeuron inputNeuron)
        {
            this.inputNeuron = inputNeuron;
        }

        public override void Apply(NeuronList neuronList)
        {
            neuronList.Add(inputNeuron);
        }
    }
}