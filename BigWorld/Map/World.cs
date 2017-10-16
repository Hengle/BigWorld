using System.Collections.Generic;
using engenious;

namespace BigWorld.Map
{
    public class World
    {
        private readonly Dictionary<Point,Room> roomList = new Dictionary<Point, Room>();

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
    }
}