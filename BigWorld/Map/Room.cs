using System;
using System.Collections.Generic;
using System.IO;
using engenious;

namespace BigWorld.Map
{
    public class Room
    {
        public const int SizeX = 16;
        public const int SizeY = 16;
        public const int MaxRoomLights = 8;
        
        public readonly Point Point;
        
        public Layer<bool> BlockLayer { get; private set; } = new Layer<bool>();
        public List<Layer<uint>> TileLayers {get; private set;} = new List<Layer<uint>>();
        
        public readonly RoomLight[] RoomLights = new RoomLight[MaxRoomLights];
        
        public Color AmbientColor { get; set; } = Color.White;
        public float AmbientIntensity { get; set; } = 1;
        
        public Room(Point point)
        {
            Point = point;
            //GroundValue
            TileLayers.Add(new Layer<uint>());

            AmbientIntensity = 0.5f;
            
            RoomLights[0] = new RoomLight(Color.Red,new Vector2(8,8));
        }

        public Layer<uint> this[int index]
        {
            get { return GetLayer(index); }
        }
        
        public Layer<uint> GetLayer(int index)
        {
            if (index < 0)
                throw new IndexOutOfRangeException();
            
            if (TileLayers.Count <= index)
            {
                var layer = new Layer<uint>();
                
                TileLayers.Add(layer);
                
                return layer;
            }

            return TileLayers[index];
        }


        internal void Serialize(BinaryWriter sw)
        {
            sw.Write(Point.X);
            sw.Write(Point.Y);
            
            sw.Write(AmbientIntensity);
            sw.Write(AmbientColor.A);
            sw.Write(AmbientColor.R);
            sw.Write(AmbientColor.G);
            sw.Write(AmbientColor.B);

            BlockLayer.Serialize(sw, (e,s) => s.Write(e));
            
            sw.Write(TileLayers.Count);

            foreach (var tileLayer in TileLayers)
            {
                tileLayer.Serialize(sw,(e,s) => s.Write(e));
            }
        }

        internal static Room Deserialize(BinaryReader sr)
        {
            var x = sr.ReadInt32();
            var y = sr.ReadInt32();
            
            var room = new Room(new Point(x,y));
            room.AmbientIntensity = sr.ReadSingle();

            var a = sr.ReadSingle();
            var r = sr.ReadSingle();
            var g = sr.ReadSingle();
            var b = sr.ReadSingle();
            
            room.AmbientColor = new Color(r,g,b,a);
            
            var blockLayer = new Layer<bool>();
            blockLayer.Deserialize(sr,s => s.ReadBoolean());
            room.BlockLayer = blockLayer;

            room.TileLayers.Clear();
            
            var layers = sr.ReadInt32();
            for (int i = 0; i < layers; i++)
            {
                var layer = new Layer<uint>();
                layer.Deserialize(sr, s => s.ReadUInt32());
                room.TileLayers.Add(layer);
            }

            return room;
        }
    }
}