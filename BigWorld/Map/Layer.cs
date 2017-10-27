using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using engenious;

namespace BigWorld.Map
{
    public class Layer<T> where T : struct 
    {
        public readonly T?[] Values = new T?[Room.SizeX* Room.SizeY];

        public T? GetValue(Point point)
        {
            return GetValue(point.X, point.Y);
        }

        public T? GetValue(int x, int y)
        {
            var flatIndex = GetFlatIndex(x, y);

            return Values[flatIndex];
        }
        
        public void SetValue(Point point,T? value)
        {
            SetValue(point.X,point.Y,value);
        }
        
        public void SetValue(int x, int y,T? value)
        {
            var flatIndex = GetFlatIndex(x, y);

            Values[flatIndex] = value;
        }

        public int GetFlatIndex(int x, int y)
        {
            if (x < 0 || x > Room.SizeX)
                throw new IndexOutOfRangeException("X out of Range");
            
            if (y < 0 || y > Room.SizeY)
                throw new IndexOutOfRangeException("Y out of Range");

            return y * Room.SizeY + x;
        }

        public Point GetPointByIndex(int index)
        {
            var x = index % Room.SizeY;
            var y = index / Room.SizeY;
            
            return new Point(x,y);
        }

        public IEnumerable<KeyValuePair<Point,T>> GetPositivValues()
        {
            for (int i = 0; i < Values.Length; i++)
            {
                var value = Values[i];
                if (value.HasValue)
                {
                    yield return new KeyValuePair<Point, T>(GetPointByIndex(i),value.Value);
                }
            }
        }

        internal void Serialize(BinaryWriter sw,Action<T,BinaryWriter> serialize)
        {
            for (int i = 0; i < Values.Length; i++)
            {
                var element = Values[i];
                sw.Write(element.HasValue);
                if (element.HasValue)
                {
                    serialize(element.Value,sw);
                }
            }
        }

        internal void Deserialize(BinaryReader sr,Func<BinaryReader,T> deserialize)
        {
            for (int i = 0; i < Values.Length; i++)
            {
                var value = sr.ReadBoolean();

                if (value)
                {
                    Values[i] = deserialize(sr);
                }
            }
        }
    }
}