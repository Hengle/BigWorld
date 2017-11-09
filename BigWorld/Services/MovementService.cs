using BigWorld.Entities;
using BigWorld.Entities.Components;
using BigWorld.Map;
using engenious;

namespace BigWorld.Services
{
    public class MovementService : BaseServiceR2<MovementComponent,PositionComponent>
    {
        protected override void Update(MovementComponent comp1, PositionComponent comp2,
            Entity entity, WorldMap worldMap, GameTime gameTime)
        {
            comp2.RoomPosition += comp1.Velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (float.IsNaN(comp2.RoomPosition.X) || float.IsNaN(comp2.RoomPosition.Y) )
            {
                
            }
        }
    }
}