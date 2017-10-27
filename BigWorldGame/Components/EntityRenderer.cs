using System;
using System.Collections.Generic;
using System.Linq;
using BigWorld;
using BigWorld.Map;
using BigWorldGame.Graphics;
using engenious;
using engenious.Graphics;

namespace BigWorldGame.Components
{
    public class EntityRenderer : DrawableGameComponent
    {
        private VertexBuffer vertexBuffer;
        private IndexBuffer indexBuffer;
        private Spritesheet spriteSheet;

        private Effect effect;

        static SamplerState NearestSampler = new SamplerState()
        {
            TextureFilter = TextureFilter.Nearest,
            AddressU = TextureWrapMode.Repeat,
            AddressV = TextureWrapMode.Repeat
        };

        public new readonly MainGame Game;
        
        public EntityRenderer(MainGame game) : base(game)
        {
            Game = game;
        }

        protected override void LoadContent()
        {
            spriteSheet = new Spritesheet(GraphicsDevice,Game.Content,"Spritesheets/TileSheetCharacter",16,16);
            effect = Game.Content.Load<Effect>("simple");
        }

        public override void Update(GameTime gameTime)
        {
            List<CharacterVertex> vertices = new List<CharacterVertex>(Room.SizeX * Room.SizeY * 4);

            var entities = Game.CurrentGameState == GameState.Running
                ? Game.SimulationComponent.Simulation.Entities
                : null;

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    float x = entity.RoomPosition.X;
                    float y = entity.RoomPosition.Y;
            
                    uint index = 0;
            
                    vertices.Add(new CharacterVertex(x + 0, y + 0, 0, 0, 0, index));
                    vertices.Add(new CharacterVertex(x + 1, y + 0, 0, 1, 0, index));
            
                    vertices.Add(new CharacterVertex(x + 0, y + 1, 0, 0, 1, index));
                    vertices.Add(new CharacterVertex(x + 1, y + 1, 0, 1, 1, index));
                }
            }
            
            if (vertexBuffer == null)
                vertexBuffer = new VertexBuffer(Game.GraphicsDevice, CharacterVertex.VertexDeclaration, vertices.Count);
            else if (vertexBuffer.VertexCount != vertices.Count)
                vertexBuffer.Resize(vertices.Count);

            vertexBuffer.SetData(vertices.ToArray());
            CreateIndexBuffer(Game.GraphicsDevice, vertexBuffer.VertexCount / 4);
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

        public override void Draw(GameTime gameTime)
        {
            Matrix projection = Matrix.CreateOrthographic(
                GraphicsDevice.Viewport.Width, 
                GraphicsDevice.Viewport.Height,-1.1f, 10);

            var posX = 16*Room.SizeX;
            var posY = 16*Room.SizeY;
            
            Matrix view = Matrix.CreateLookAt(new Vector3(posX,posY,10),new Vector3(posX,posY,0), Vector3.UnitY) 
                          * Matrix.CreateScaling(new Vector3(2));
            
            Matrix world = Matrix.CreateTranslation(0,0,0) * Matrix.CreateScaling(16,16,0);
            
            //_spriteSheets
            GraphicsDevice.IndexBuffer = indexBuffer;
            GraphicsDevice.VertexBuffer = vertexBuffer;

            spriteSheet.Textures.SamplerState = NearestSampler;
            effect.Parameters["TileTextures"].SetValue(spriteSheet.Textures);
            effect.Parameters["WorldViewProj"].SetValue(projection * view *  world);
            
            
            if (Game.CurrentGameState == GameState.Build)
            {
                effect.CurrentTechnique = effect.Techniques["Build"];
            }
            else if (Game.CurrentGameState == GameState.Debug || Game.CurrentGameState == GameState.Running)
            {
                effect.CurrentTechnique = effect.Techniques["RunCharacter"];
                
                effect.Parameters["AmbientColor"].SetValue(Color.White);
                effect.Parameters["AmbientIntensity"].SetValue(0.2f);
                effect.Parameters["LightPosition"].SetValue(new Vector2(7,8));
            }

            foreach (var pass in effect.CurrentTechnique.Passes.PassesList)
            {
                pass.Apply();

                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.Triangles, 0, 0, vertexBuffer.VertexCount, 0,
                    indexBuffer.IndexCount / 3);
            }
        }
    }
}