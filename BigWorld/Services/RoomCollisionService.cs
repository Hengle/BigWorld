using BigWorld.Entities;
using BigWorld.Entities.Components;
using BigWorld.Map;
using engenious;

namespace BigWorld.Services
{
    public class RoomCollisionService : BaseServiceR1<PositionComponent>
    {
        protected override void Update(PositionComponent comp, Entity entity, WorldMap worldMap, GameTime gameTime)
        {
            if (comp.RoomPosition.X < 0)
            {
                comp.RoomPosition = new Vector2(0,comp.RoomPosition.Y);

                var position = new Point(Room.SizeX -1 ,(int)comp.RoomPosition.Y);
                var newRoomCoordinate = comp.CurrentRoom +new Point(-1,0);
                if (worldMap.TryGetRoom(newRoomCoordinate,out Room room) 
                    && room.BlockLayer.Test(position,i => !i,true))
                {
                    comp.CurrentRoom = newRoomCoordinate;
                    comp.RoomPosition = position.ToVector2();
                }
            }
            else if (comp.RoomPosition.X > Room.SizeX -1)
            {
                comp.RoomPosition = new Vector2(Room.SizeX -1,comp.RoomPosition.Y);
                
                var position = new Point(0 ,(int)comp.RoomPosition.Y);
                var newRoomCoordinate = comp.CurrentRoom +new Point(1,0);
                if (worldMap.TryGetRoom(newRoomCoordinate,out Room room) 
                    && room.BlockLayer.Test(position,i => !i,true))
                {
                    comp.CurrentRoom = newRoomCoordinate;
                    comp.RoomPosition = position.ToVector2();
                }
            }

            if (comp.RoomPosition.Y < 0)
            {
                comp.RoomPosition = new Vector2(comp.RoomPosition.X,0);
                
                var position = new Point((int)comp.RoomPosition.X,Room.SizeY -1 );
                var newRoomCoordinate = comp.CurrentRoom +new Point(0,-1);
                if (worldMap.TryGetRoom(newRoomCoordinate,out Room room) 
                    && room.BlockLayer.Test(position,i => !i,true))
                {
                    comp.CurrentRoom = newRoomCoordinate;
                    comp.RoomPosition = position.ToVector2();
                }
            }
            else if (comp.RoomPosition.Y > Room.SizeY -1)
            {
                comp.RoomPosition = new Vector2(comp.RoomPosition.X,Room.SizeY -1);
                
                var position = new Point((int)comp.RoomPosition.X,0 );
                var newRoomCoordinate = comp.CurrentRoom +new Point(0,1);
                if (worldMap.TryGetRoom(newRoomCoordinate,out Room room) 
                    && room.BlockLayer.Test(position,i => !i,true))
                {
                    comp.CurrentRoom = newRoomCoordinate;
                    comp.RoomPosition = position.ToVector2();
                }
            }
        }
    }
}