using System.Collections.Generic;
using BigWorld.Map;
using engenious;
using engenious.Graphics;

namespace BigWorldGame.Graphics
{
    public class RoomRenderer
    {
        private VertexBuffer vertexBuffer;
        private IndexBuffer indexBuffer;
        private readonly Spritesheet spriteSheet;

        static SamplerState NearestSampler = new SamplerState()
        {
            TextureFilter = TextureFilter.Nearest,
            AddressU = TextureWrapMode.Repeat,
            AddressV = TextureWrapMode.Repeat
        };

        private Game game;
        private Matrix world;

        public RoomRenderer(Game game,Point point, Spritesheet spriteSheet)
        {
            this.game = game;
            this.spriteSheet = spriteSheet;
            
            world = Matrix.CreateTranslation(point.X * Room.SizeX * 16,point.Y * Room.SizeY * 16,0)* Matrix.CreateScaling(16, 16, 16);
        }


        public void ReloadChunk(Room room)
        {
            List<MapVertex> vertices = new List<MapVertex>(Room.SizeX * Room.SizeY * 4);

            int l = 0;
            
            foreach (var layer in room.TileLayer)
            {   
                for (int x = 0; x < Room.SizeX; x++)
                {
                    for (int y = 0; y < Room.SizeY; y++)
                    {
                        var tileIndex = layer.GetValue(x,y);

                        if (tileIndex.HasValue)
                        {                            
                            var index = tileIndex.Value;
                            vertices.Add(new MapVertex(x + 0, y + 0, l, index));
                            vertices.Add(new MapVertex(x + 1, y + 0, l, index));

                            vertices.Add(new MapVertex(x + 0, y + 1, l, index));
                            vertices.Add(new MapVertex(x + 1, y + 1, l, index));
                        }
                    }
                }
                l++;
            }
            
            

            if (vertexBuffer == null)
                vertexBuffer = new VertexBuffer(game.GraphicsDevice, MapVertex.VertexDeclaration, vertices.Count);
            else if (vertexBuffer.VertexCount != vertices.Count)
                vertexBuffer.Resize(vertices.Count);

            vertexBuffer.SetData(vertices.ToArray());
            CreateIndexBuffer(game.GraphicsDevice, vertexBuffer.VertexCount / 4);
        }


        private void CreateIndexBuffer(GraphicsDevice graphicsDevice, int quadCount)
        {
            List<ushort> indices = new List<ushort>(quadCount * 6);
            for (uint i = 0; i < quadCount * 4; i += 4)
            {
                indices.Add((ushort) (0 + i));
                indices.Add((ushort) (1 + i));
                indices.Add((ushort) (3 + i));

                indices.Add((ushort) (0 + i));
                indices.Add((ushort) (3 + i));
                indices.Add((ushort) (2 + i));
            }
            if (indexBuffer == null)
                indexBuffer = new IndexBuffer(graphicsDevice, DrawElementsType.UnsignedShort, indices.Count);
            else
                indexBuffer.Resize(indices.Count);

            indexBuffer.SetData(indices.ToArray());
        }

        public void Render(GraphicsDevice graphicsDevice, Effect effect, Matrix view, Matrix projection)
        {
            //_spriteSheets
            graphicsDevice.IndexBuffer = indexBuffer;
            graphicsDevice.VertexBuffer = vertexBuffer;

            spriteSheet.Textures.SamplerState = NearestSampler;
            effect.Parameters["TileTextures"].SetValue(spriteSheet.Textures);
            effect.Parameters["WorldViewProj"].SetValue(projection * view *  world);
            

            foreach (var pass in effect.CurrentTechnique.Passes.PassesList)
            {
                pass.Apply();

                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.Triangles, 0, 0, vertexBuffer.VertexCount, 0,
                    indexBuffer.IndexCount / 3);
            }
        }
    }
}