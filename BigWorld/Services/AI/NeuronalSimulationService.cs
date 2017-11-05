using BigWorld.Entities;
using BigWorld.Entities.Components;
using BigWorld.Entities.Components.AI;
using BigWorld.Map;
using engenious;

namespace BigWorld.Services.AI
{
    public class NeuronalSimulationService : BaseServiceR2<NeuronalNetworkComponent,InputComponent>
    {
        protected override void Update(NeuronalNetworkComponent comp1, InputComponent comp2,
              Entity entity, WorldMap worldMap, GameTime gameTime)
        {
            
        }
    }
}