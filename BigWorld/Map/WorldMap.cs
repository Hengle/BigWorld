using System;
using System.Collections.Generic;
using System.IO;
using engenious;

namespace BigWorld.Map
{
    public class WorldMap
    {
        private readonly Dictionary<Point,Room> roomList =  new Dictionary<Point, Room>();

        public Room this[Point key]
        {
            get
            {
                Room result = null;
                if (roomList.TryGetValue(key, out result))
                {
                    return result;
                }

                return null;
            }
        }

        public Room LoadOrCreateRoom(Point key)
        {
            Room result = null;
            if (!roomList.TryGetValue(key, out result))
            {
                result = new Room(key);
                roomList[key] = result;
            }
            return result;
        }

        public bool TryGetRoom(Point currentRoomCoordinate, out Room room)
        {
            return roomList.TryGetValue(currentRoomCoordinate, out room);
        }

        public void SaveWorld()
        {
            using (var fs = File.Open("world.dat",FileMode.Create,FileAccess.Write))
            using (var sw = new BinaryWriter(fs))
            {
                sw.Write(roomList.Count);
                foreach (var room in roomList.Values)
                {
                    room.Serialize(sw);
                }
            }
        }    

        public static WorldMap LoadWorld()
        {
            WorldMap world = new WorldMap();
            
            using (var fs = File.Open("world.dat",FileMode.Open,FileAccess.Read))
            using (var sr = new BinaryReader(fs))
            {
                var roomCount = sr.ReadInt32();

                for (int i = 0; i < roomCount; i++)
                {
                    var room = Room.Deserialize(sr);
                    world.roomList.Add(room.Point,room);
                }
            }

            return world;
        }
    }
}