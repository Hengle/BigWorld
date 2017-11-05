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
            if (!comp1.Enable)
                return;
            
            //TickNeuron
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if(comp1.Tick.Value == 0)
                comp1.Tick.SetValue(1);
            
            comp1.Tick.SetValue(-1* comp1.Tick.Value);
            
            //Update
            comp1.NeuronList.Update();
            
            comp2.MoveDirection = new Vector2(comp1.MoveRight.Value - comp1.MoveLeft.Value
                                              ,comp1.MoveUp.Value - comp1.MoveDown.Value );
        }
    }
}