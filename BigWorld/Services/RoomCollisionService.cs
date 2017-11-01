using BigWorld.Entities;
using BigWorld.Entities.Components;
using BigWorld.Map;
using engenious;

namespace BigWorld.Services
{
    public class RoomCollisionService : BaseService
    {
        public override void Update(Entity entity, WorldMap worldMap, GameTime gameTime)
        {
               
            PositionComponent positionComponent;
            if (!entity.TryGetComponent<PositionComponent>(out positionComponent) )
                return;
               
            if (positionComponent.RoomPosition.X < 0)
            {
                positionComponent.RoomPosition = new Vector2(0,positionComponent.RoomPosition.Y);

                var position = new Point(Room.SizeX -1 ,(int)positionComponent.RoomPosition.Y);
                var newRoomCoordinate = positionComponent.CurrentRoom +new Point(-1,0);
                if (worldMap.TryGetRoom(newRoomCoordinate,out Room room) 
                    && room.BlockLayer.Test(position,i => !i,true))
                {
                    positionComponent.CurrentRoom = newRoomCoordinate;
                    positionComponent.RoomPosition = position.ToVector2();
                }
            }
            else if (positionComponent.RoomPosition.X > Room.SizeX -1)
            {
                positionComponent.RoomPosition = new Vector2(Room.SizeX -1,positionComponent.RoomPosition.Y);
                
                var position = new Point(0 ,(int)positionComponent.RoomPosition.Y);
                var newRoomCoordinate = positionComponent.CurrentRoom +new Point(1,0);
                if (worldMap.TryGetRoom(newRoomCoordinate,out Room room) 
                    && room.BlockLayer.Test(position,i => !i,true))
                {
                    positionComponent.CurrentRoom = newRoomCoordinate;
                    positionComponent.RoomPosition = position.ToVector2();
                }
            }

            if (positionComponent.RoomPosition.Y < 0)
            {
                positionComponent.RoomPosition = new Vector2(positionComponent.RoomPosition.X,0);
                
                var position = new Point((int)positionComponent.RoomPosition.X,Room.SizeY -1 );
                var newRoomCoordinate = positionComponent.CurrentRoom +new Point(0,-1);
                if (worldMap.TryGetRoom(newRoomCoordinate,out Room room) 
                    && room.BlockLayer.Test(position,i => !i,true))
                {
                    positionComponent.CurrentRoom = newRoomCoordinate;
                    positionComponent.RoomPosition = position.ToVector2();
                }
            }
            else if (positionComponent.RoomPosition.Y > Room.SizeY -1)
            {
                positionComponent.RoomPosition = new Vector2(positionComponent.RoomPosition.X,Room.SizeY -1);
                
                var position = new Point((int)positionComponent.RoomPosition.X,0 );
                var newRoomCoordinate = positionComponent.CurrentRoom +new Point(0,1);
                if (worldMap.TryGetRoom(newRoomCoordinate,out Room room) 
                    && room.BlockLayer.Test(position,i => !i,true))
                {
                    positionComponent.CurrentRoom = newRoomCoordinate;
                    positionComponent.RoomPosition = position.ToVector2();
                }
            }
        }
    }
}