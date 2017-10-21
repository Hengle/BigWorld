using engenious;
using engenious.Graphics;

namespace BigWorldGame.Graphics
{
    public struct MapVertex : IVertexType
    {
        public static readonly VertexDeclaration VertexDeclaration;
        static MapVertex()
        {
            VertexDeclaration = new VertexDeclaration(sizeof(float) * 3 + sizeof(uint), new[] { new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0), new VertexElement(sizeof(float) * 3, VertexElementFormat.Rgba32, VertexElementUsage.Normal, 0) });
        }
        public MapVertex(int x, int y, int z, uint tileIndex)
        {
            Position = new Vector3(x, y, z);
            TileIndex = tileIndex;
        }
        public Vector3 Position { get; }
        public uint TileIndex { get; }

        VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;
    }
}