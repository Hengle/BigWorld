using System.Security.Policy;
using engenious;

namespace BigWorld.Map
{
    public struct RoomLight
    {
        public Color LightColor;
        public Vector2 Position;
        public float Radius;
        public bool Enable;

        public RoomLight(Color lightColor,Vector2 position,float radius = 1)
        {
            LightColor = lightColor;
            Position = position;
            Radius = radius;
            Enable = true;
        }
    }
}