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

            foreach (var value in room.BlockLayer.GetPositivValues())
            {
                if (value.Value)
                {
                    var distance = entity.RoomPosition - value.Key.ToVector2();

                    if ((distance.X > -1 && distance.X < 1) && (distance.Y > -1 && distance.Y < 1))
                    {
                        float x = distance.X < 0 ? distance.X : 1 - distance.X;
                        float y = distance.Y < 0 ? 1 + distance.Y : 1 - distance.Y;
                        
                        var corr = new Vector2(x, y);
                        entity.RoomPosition += corr;
                    }
                }
            }
        }
    }
}