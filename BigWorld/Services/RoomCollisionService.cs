using BigWorld.Map;
using engenious;

namespace BigWorld.Services
{
    public class RoomCollisionService : BaseService
    {
        public override void Update(Entity entity, WorldMap worldMap, GameTime gameTime)
        {
            if (entity.RoomPosition.X < 0)
            {
                entity.RoomPosition = new Vector2(0,entity.RoomPosition.Y);
            }
            else if (entity.RoomPosition.X > Room.SizeX -1)
            {
                entity.RoomPosition = new Vector2(Room.SizeX -1,entity.RoomPosition.Y);
            }

            if (entity.RoomPosition.Y < 0)
            {
                entity.RoomPosition = new Vector2(entity.RoomPosition.X,0);
            }
            else if (entity.RoomPosition.Y > Room.SizeY -1)
            {
                entity.RoomPosition = new Vector2(entity.RoomPosition.X,Room.SizeY -1);
            }
        }
    }
}