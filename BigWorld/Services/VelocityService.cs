using BigWorld.Entities;
using BigWorld.Entities.Components;
using BigWorld.Map;
using engenious;

namespace BigWorld.Services
{
    public class VelocityService : BaseServiceR2<MovementComponent,InputComponent>
    {
        public override void Update(Entity entity, WorldMap worldMap, GameTime gameTime)
        {
        }

        protected override void Update(MovementComponent comp1, InputComponent comp2,
            Entity entity,WorldMap worldMap, GameTime gameTime)
        {
            comp1.Velocity = comp2.MoveDirection  * 4;
        }
    }
}