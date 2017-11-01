using BigWorld.Entities;
using BigWorld.Entities.Components;
using BigWorld.Map;
using engenious;

namespace BigWorld.Services
{
    public class VelocityService : BaseService
    {
        public override void Update(Entity entity, WorldMap worldMap, GameTime gameTime)
        {
            MovementComponent movementComponent;
            InputComponent inputComponent;

            if (!entity.TryGetComponent<MovementComponent>(out movementComponent) ||
                !entity.TryGetComponent<InputComponent>(out inputComponent))
                return;
            
            movementComponent.Velocity = inputComponent.MoveDirection  * 4;
        }
    }
}