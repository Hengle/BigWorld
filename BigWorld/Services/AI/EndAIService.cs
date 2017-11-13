using BigWorld.Entities;
using BigWorld.Entities.Components;
using BigWorld.Entities.Components.AI;
using BigWorld.Map;
using engenious;

namespace BigWorld.Services.AI
{
    public class EndAIService : BaseServiceR2<AIInformationComponent,FitnessComponent>
    {
        protected override void Update(AIInformationComponent comp1, FitnessComponent comp2, Entity entity, WorldMap worldMap, GameTime gameTime)
        {
            if (entity.TryGetComponent<PositionComponent>(out var position))
            {
                var distance = position.RoomPosition - comp1.OldPosition;

                if (distance.LengthSquared > 0)
                {
                    comp2.Value += distance.Length *10;
                    
                    distance.Normalize();
                    if (entity.TryGetComponent<NeuronalNetworkComponent>(out var networkComponent))
                    {
                        networkComponent.DeltaPositionX.SetValue(distance.X);
                        networkComponent.DeltaPositionY.SetValue(distance.Y);
                    }
                }
                else
                {
                    if (entity.TryGetComponent<NeuronalNetworkComponent>(out var networkComponent))
                    {
                        networkComponent.DeltaPositionX.SetValue(0);
                        networkComponent.DeltaPositionY.SetValue(0);
                    }
                }
                

            }
        }
    }
}