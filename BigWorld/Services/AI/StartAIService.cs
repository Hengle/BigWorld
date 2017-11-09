using BigWorld.Entities;
using BigWorld.Entities.Components;
using BigWorld.Entities.Components.AI;
using BigWorld.Map;
using engenious;

namespace BigWorld.Services.AI
{
    public class StartAIService : BaseServiceR2<AIInformationComponent,FitnessComponent>
    {
        protected override void Update(AIInformationComponent comp1, FitnessComponent comp2, Entity entity, WorldMap worldMap, GameTime gameTime)
        {
            if (entity.TryGetComponent<PositionComponent>(out var position))
            {
                comp1.OldPosition = position.RoomPosition;
            }
        }
    }
}