using System.Security.Policy;
using engenious;

namespace BigWorld.Map
{
    public struct RoomLight
    {   
        public Color Color;
        public Vector2 Position;
        public float Radius;
        public bool Enable;

        public RoomLight(Color color,Vector2 position,float radius = 1)
        {
            Color = color;
            Position = position;
            Radius = radius;
            Enable = true;
        }
    }
}