using System.IO;
using engenious;

namespace BigWorld.Map
{
    public struct RoomLight
    {
        public Color Color;
        public Vector2 Position;
        public float Radius;
        public bool Enable;

        public RoomLight(Color color, Vector2 position, float radius = 1)
        {
            Color = color;
            Position = position;
            Radius = radius;
            Enable = true;
        }

        internal void Serialize(BinaryWriter bw)
        {
            bw.Write(Color.A);
            bw.Write(Color.R);
            bw.Write(Color.G);
            bw.Write(Color.B);

            bw.Write(Position.X);
            bw.Write(Position.Y);

            bw.Write(Radius);
            bw.Write(Enable);
        }

        internal void Deserialize(BinaryReader bw)
        {
            var a = bw.ReadSingle();
            var r = bw.ReadSingle();
            var g = bw.ReadSingle();
            var b = bw.ReadSingle();
            Color = new Color(r, g, b, a);

            var x = bw.ReadSingle();
            var y = bw.ReadSingle();
            Position = new Vector2(x, y);

            Radius = bw.ReadSingle();
            Enable = bw.ReadBoolean();

        }

    }
}