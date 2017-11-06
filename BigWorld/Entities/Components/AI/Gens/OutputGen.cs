namespace BigWorld.Entities.Components.AI.Gens
{
    public class OutputGen : NeuronGen
    {
        private readonly OutputNeuron outputNeuron;

        public OutputGen(OutputNeuron outputNeuron)
        {
            this.outputNeuron = outputNeuron;
        }

        public override void Apply(NeuronList neuronList)
        {
            neuronList.Add(outputNeuron);
        }
    }
}