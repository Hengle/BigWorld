using System;
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

        public Layer<uint> this[int index]
        {
            get { return GetLayer(index); }
        }
        
        public Layer<uint> GetLayer(int index)
        {
            if (index < 0)
                throw new IndexOutOfRangeException();
            
            if (TileLayer.Count <= index)
            {
                var layer = new Layer<uint>();
                
                TileLayer.Add(layer);
                
                return layer;
            }

            return TileLayer[index];
        }
        
        
    }
}