using BigWorld.Entities.Components.AI.Gens;

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
        
        

        public NeuronList NeuronList { get; private set; } = new NeuronList();
        private Genome genome;
        
        public NeuronalNetworkComponent()
        {
            Enable = true;
            
            //Input
            Tick = new InputNeuron();
            
            //Output
            MoveUp = new OutputNeuron();
            MoveDown = new OutputNeuron();
            MoveLeft = new OutputNeuron();
            MoveRight = new OutputNeuron();
            
            //Genom
            genome = new Genome(12685);
            genome.Add(new InputGen(Tick));
            
            genome.Add(new OutputGen(MoveUp));
            genome.Add(new OutputGen(MoveDown));
            genome.Add(new OutputGen(MoveLeft));
            genome.Add(new OutputGen(MoveRight));
            
            //genome.CreateRandomGens(10);
            
            NeuronList.CreateNeuronsFromGenome(genome);
        }

        public void Mutate()
        {
            genome.Mutate();
            NeuronList = new NeuronList();
            NeuronList.CreateNeuronsFromGenome(genome);
        }
    }
}