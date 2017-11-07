using BigWorld.Entities;
using BigWorld.Entities.Components;
using BigWorld.Entities.Components.AI;
using BigWorld.Map;
using engenious;

namespace BigWorld.Services
{
    public class VelocityService : BaseServiceR2<MovementComponent,InputComponent>
    {
        protected override void Update(MovementComponent comp1, InputComponent comp2,
            Entity entity,WorldMap worldMap, GameTime gameTime)
        {
            comp1.Velocity = comp2.MoveDirection  * 4;

            if (entity.TryGetComponent<FitnessComponent>(out var fitComp))
            {
                fitComp.Value += comp1.Velocity.Length;
            }
            
        }
    }
}