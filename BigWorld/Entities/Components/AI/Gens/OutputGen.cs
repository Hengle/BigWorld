namespace BigWorld.Entities.Components.AI.Gens
{
    public class OutputGen : NeuronGen
    {
        public OutputNeuron OutputNeuron { get; set; }

        public OutputGen(OutputNeuron outputNeuron)
        {
            this.OutputNeuron = outputNeuron;
        }

        public override void Apply(NeuronList neuronList)
        {
            neuronList.Add(OutputNeuron);
        }
        
        public override Gen Copy()
        {
            return new OutputGen(OutputNeuron);
        }
    }
}