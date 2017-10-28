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

                var position = new Point(Room.SizeX -1 ,(int)entity.RoomPosition.Y);
                var newRoomCoordinate = entity.CurrentRoom +new Point(-1,0);
                if (worldMap.TryGetRoom(newRoomCoordinate,out Room room) 
                    && room.BlockLayer.Test(position,i => !i,true))
                {
                    entity.CurrentRoom = newRoomCoordinate;
                    entity.RoomPosition = position.ToVector2();
                }
            }
            else if (entity.RoomPosition.X > Room.SizeX -1)
            {
                entity.RoomPosition = new Vector2(Room.SizeX -1,entity.RoomPosition.Y);
                
                var position = new Point(0 ,(int)entity.RoomPosition.Y);
                var newRoomCoordinate = entity.CurrentRoom +new Point(1,0);
                if (worldMap.TryGetRoom(newRoomCoordinate,out Room room) 
                    && room.BlockLayer.Test(position,i => !i,true))
                {
                    entity.CurrentRoom = newRoomCoordinate;
                    entity.RoomPosition = position.ToVector2();
                }
            }

            if (entity.RoomPosition.Y < 0)
            {
                entity.RoomPosition = new Vector2(entity.RoomPosition.X,0);
                
                var position = new Point((int)entity.RoomPosition.X,Room.SizeY -1 );
                var newRoomCoordinate = entity.CurrentRoom +new Point(0,-1);
                if (worldMap.TryGetRoom(newRoomCoordinate,out Room room) 
                    && room.BlockLayer.Test(position,i => !i,true))
                {
                    entity.CurrentRoom = newRoomCoordinate;
                    entity.RoomPosition = position.ToVector2();
                }
            }
            else if (entity.RoomPosition.Y > Room.SizeY -1)
            {
                entity.RoomPosition = new Vector2(entity.RoomPosition.X,Room.SizeY -1);
                
                var position = new Point((int)entity.RoomPosition.X,0 );
                var newRoomCoordinate = entity.CurrentRoom +new Point(0,1);
                if (worldMap.TryGetRoom(newRoomCoordinate,out Room room) 
                    && room.BlockLayer.Test(position,i => !i,true))
                {
                    entity.CurrentRoom = newRoomCoordinate;
                    entity.RoomPosition = position.ToVector2();
                }
            }
        }
    }
}