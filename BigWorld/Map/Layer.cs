using System;
using engenious;

namespace BigWorld.Map
{
    public class Layer<T>
        where T : struct 
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
    }
}