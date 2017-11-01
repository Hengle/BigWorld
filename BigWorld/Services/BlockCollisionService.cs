using System.Runtime.ExceptionServices;
using BigWorld.Entities;
using BigWorld.Entities.Components;
using BigWorld.Map;
using engenious;

namespace BigWorld.Services
{
    public class BlockCollisionService : BaseService
    {
        public override void Update(Entity entity, WorldMap worldMap, GameTime gameTime)
        {
            MovementComponent movementComponent;
            PositionComponent positionComponent;

            if (!entity.TryGetComponent<MovementComponent>(out movementComponent) 
                || !entity.TryGetComponent<PositionComponent>(out positionComponent) )
                return;
             
            
            Room room;
            if (!worldMap.TryGetRoom(positionComponent.CurrentRoom,out room))
                return;

            var goalPosition = movementComponent.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds 
                               + positionComponent.RoomPosition;
            
            foreach (var value in room.BlockLayer.GetPositivValues())
            {
                if (value.Value)
                {
                    var distance = goalPosition - value.Key.ToVector2();
                    
                    if ((distance.X > -1 && distance.X < 1) && (distance.Y > -1 && distance.Y < 1))
                    {
                        //var x = distance.X < 0 ? 1 + distance.X : 1 - distance.X;
                        //var y = distance.Y < 0 ? 1 + distance.Y : 1 - distance.Y;

                        movementComponent.Velocity = movementComponent.Velocity * new Vector2(0, 0);
                    }
                }
            }
        }
    }
}