namespace BigWorld.Entities.Components.AI.Gens
{
    public class HiddenNeuronGen : NeuronGen
    {
        public override Gen Copy()
        {
            return new HiddenNeuronGen();
        }

        public override void Apply(NeuronList neuronList)
        {
            neuronList.CreateNeuron<HiddenNeuron>();
        }
    }
}