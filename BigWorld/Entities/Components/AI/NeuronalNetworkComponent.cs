using BigWorld.Entities.Components.AI.Gens;

namespace BigWorld.Entities.Components.AI
{
    public class NeuronalNetworkComponent : Component
    {
        public bool Enable { get; set; }
        
        public readonly InputNeuron Tick;
        public readonly InputNeuron Const;
        
        public readonly InputNeuron DeltaPositionX;
        public readonly InputNeuron DeltaPositionY;

        public readonly OutputNeuron MoveUp;
        public readonly OutputNeuron MoveDown;
        public readonly OutputNeuron MoveLeft;
        public readonly OutputNeuron MoveRight;
        
        

        public NeuronList NeuronList { get; private set; } = new NeuronList();
        public Genome Genome { get; private set; }
        
        public NeuronalNetworkComponent()
        {
            Enable = true;
            
            //Input
            Tick = new InputNeuron();
            Const = new InputNeuron();
            DeltaPositionX = new InputNeuron();
            DeltaPositionY = new InputNeuron();
            
            //Output
            MoveUp = new OutputNeuron();
            MoveDown = new OutputNeuron();
            MoveLeft = new OutputNeuron();
            MoveRight = new OutputNeuron();
        }

        public void Reset(Genome genome)
        {
            Genome = genome;

            Tick.Reset();
            MoveUp.Reset();
            MoveDown.Reset();
            MoveLeft.Reset();
            MoveRight.Reset();
            
            NeuronList = new NeuronList();
            NeuronList.CreateNeuronsFromGenome(genome);
        }
    }
}