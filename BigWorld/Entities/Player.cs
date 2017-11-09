using BigWorld.Entities;
using BigWorld.Entities.Components;
using BigWorld.Entities.Components.AI;

namespace BigWorld
{
    public class Player :Entity
    {
        public readonly PositionComponent Position;
        public readonly InputComponent Input;

        public readonly NeuronalNetworkComponent NeuronalNetwork;
        
        public Player()
        {
            NeuronalNetwork = CreateComponent<NeuronalNetworkComponent>();
            Input = CreateComponent<InputComponent>();
            
            CreateComponent<MovementComponent>();
            Position = CreateComponent<PositionComponent>();

            CreateComponent<FitnessComponent>();
            CreateComponent<AIInformationComponent>();
        }


    }
}