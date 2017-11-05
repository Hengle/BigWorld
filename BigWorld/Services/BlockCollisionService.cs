using System.Runtime.ExceptionServices;
using BigWorld.Entities;
using BigWorld.Entities.Components;
using BigWorld.Map;
using engenious;

namespace BigWorld.Services
{
    public class BlockCollisionService : BaseServiceR2<MovementComponent,PositionComponent>
    {
        protected override void Update(MovementComponent comp1, PositionComponent comp2, 
            Entity entity,WorldMap worldMap, GameTime gameTime)
        {
            Room room;
            if (!worldMap.TryGetRoom(comp2.CurrentRoom,out room))
                return;

            var goalPosition = comp1.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds 
                               + comp2.RoomPosition;
            
            foreach (var value in room.BlockLayer.GetPositivValues())
            {
                if (value.Value)
                {
                    var distance = goalPosition - value.Key.ToVector2();
                    
                    if ((distance.X > -1 && distance.X < 1) && (distance.Y > -1 && distance.Y < 1))
                    {
                        //var x = distance.X < 0 ? 1 + distance.X : 1 - distance.X;
                        //var y = distance.Y < 0 ? 1 + distance.Y : 1 - distance.Y;

                        comp1.Velocity = comp1.Velocity * new Vector2(0, 0);
                    }
                }
            }
        }
    }
}