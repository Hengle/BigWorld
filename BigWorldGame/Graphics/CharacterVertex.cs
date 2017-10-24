using engenious;
using engenious.Graphics;

namespace BigWorldGame.Graphics
{
    public struct CharacterVertex : IVertexType
    {
        public static readonly VertexDeclaration VertexDeclaration;
        
        static CharacterVertex()
        {
            VertexDeclaration = new VertexDeclaration(sizeof(float) * 3 + sizeof(float) * 3 + sizeof(uint), new[] {new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0) ,new VertexElement(sizeof(float) * 3, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0), new VertexElement(sizeof(float) * 6, VertexElementFormat.Rgba32, VertexElementUsage.Normal, 0) });
        }
        
        public CharacterVertex(float x, float y, float z,int tx, int ty, uint tileIndex)
        {
            Position = new Vector3(x, y, z);
            VertexPosition = new Vector3(tx,ty,0);
            TileIndex = tileIndex;
        }
        
        public Vector3 Position { get; }
        public Vector3 VertexPosition { get; private set; }
        public uint TileIndex { get; }

        VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;
    }
}