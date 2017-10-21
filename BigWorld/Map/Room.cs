using System.Collections.Generic;
using engenious;

namespace BigWorld.Map
{
    public class Room
    {
        public const int SizeX = 16;
        public const int SizeY = 16;
        
        public readonly Point Point;
        
        public Layer<bool> BlockLayer { get; } = new Layer<bool>();
        public List<Layer<uint>> TileLayer {get;} = new List<Layer<uint>>();
        
        public Room(Point point)
        {
            Point = point;
            //GroundValue
            TileLayer.Add(new Layer<uint>());
        }
        
        
    }
}