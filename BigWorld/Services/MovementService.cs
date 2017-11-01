using BigWorld.Entities;
using BigWorld.Entities.Components;
using BigWorld.Map;
using engenious;

namespace BigWorld.Services
{
    public class MovementService : BaseService
    {
        public override void Update(Entity entity, WorldMap worldMap, GameTime gameTime)
        {   
            MovementComponent movementComponent;
            PositionComponent positionComponent;

            if (!entity.TryGetComponent<MovementComponent>(out movementComponent) 
                || !entity.TryGetComponent<PositionComponent>(out positionComponent) )
                return;
            
            positionComponent.RoomPosition += movementComponent.Velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}