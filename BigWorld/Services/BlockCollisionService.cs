using System.Runtime.ExceptionServices;
using BigWorld.Map;
using engenious;

namespace BigWorld.Services
{
    public class BlockCollisionService : BaseService
    {
        public override void Update(Entity entity, World world, GameTime gameTime)
        {
            Room room;
            if (!world.TryGetRoom(entity.CurrentRoom,out room))
                return;

            var goalPosition = entity.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds + entity.RoomPosition;
            
            foreach (var value in room.BlockLayer.GetPositivValues())
            {
                if (value.Value)
                {
                    var distance = goalPosition - value.Key.ToVector2();
                    
                    if ((distance.X > -1 && distance.X < 1) && (distance.Y > -1 && distance.Y < 1))
                    {
                        //var x = distance.X < 0 ? 1 + distance.X : 1 - distance.X;
                        //var y = distance.Y < 0 ? 1 + distance.Y : 1 - distance.Y;

                        entity.Velocity = entity.Velocity * new Vector2(0, 0);
                    }
                }
            }
        }
    }
}