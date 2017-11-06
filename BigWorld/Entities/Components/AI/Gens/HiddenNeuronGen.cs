namespace BigWorld.Entities.Components.AI.Gens
{
    [GenDefinition(1)]
    public class HiddenNeuronGen : NeuronGen
    {
        public override void Apply(NeuronList neuronList)
        {
            neuronList.CreateNeuron<HiddenNeuron>();
        }
    }
}