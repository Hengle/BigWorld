namespace BigWorld.Entities.Components.AI
{
    public class NeuronalNetworkComponent : Component
    {
        public bool Enable { get; set; }
        
        public readonly InputNeuron Tick;

        public readonly OutputNeuron MoveUp;
        public readonly OutputNeuron MoveDown;
        public readonly OutputNeuron MoveLeft;
        public readonly OutputNeuron MoveRight;
        
        public readonly NeuronList NeuronList = new NeuronList();

        public NeuronalNetworkComponent()
        {
            //Input
            Tick = NeuronList.CreateNeuron<InputNeuron>();
            
            //Output
            MoveUp = NeuronList.CreateNeuron<OutputNeuron>();
            MoveDown = NeuronList.CreateNeuron<OutputNeuron>();
            MoveLeft = NeuronList.CreateNeuron<OutputNeuron>();
            MoveRight = NeuronList.CreateNeuron<OutputNeuron>();
        }
    }
}