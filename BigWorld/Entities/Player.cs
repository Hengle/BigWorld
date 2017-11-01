using BigWorld.Entities;
using BigWorld.Entities.Components;

namespace BigWorld
{
    public class Player :Entity
    {
        public readonly PositionComponent Position;
        public readonly InputComponent Input;

        public Player()
        {
            Input = CreateComponent<InputComponent>();
            
            CreateComponent<MovementComponent>();
            Position = CreateComponent<PositionComponent>();
        }


    }
}