using System.Threading;
using BigWorld;
using BigWorld.Map;
using BigWorldGame.Graphics;
using engenious;
using engenious.Graphics;
using engenious.Input;

namespace BigWorldGame.Components
{
    public class MapRenderer : DrawableGameComponent
    {
        public new readonly MainGame Game;

        private Texture2D pixeltexture;

        private Spritesheet spriteSheet;
        
        private Effect effect;


        private RoomRenderer[] renderers = new RoomRenderer[9];
        
        public MapRenderer(MainGame game) : base(game)
        {
            this.Game = game;
        }


        protected override void LoadContent()
        {
            
            pixeltexture = new Texture2D(GraphicsDevice,1,1);
            pixeltexture.SetData(new Color[] {Color.White});

            
            spriteSheet = new Spritesheet(Game.GraphicsDevice,Game.Content,"Spritesheets/TileSheetDungeon",16,16);
            
            effect = Game.Content.Load<Effect>("simple");

            for (int i = 0; i < renderers.Length; i++)
            {
                var x = i % 3 - 1;
                var y = i / 3 - 1;
                
                renderers[i] = new RoomRenderer(Game,new Point(x,y), spriteSheet);
            }
        }


        
        
        public override void Update(GameTime gameTime)
        {
            
            for (int i = 0; i < renderers.Length; i++)
            {
                var x = i % 3 - 1;
                var y = i / 3 - 1;

                var room = Game.SimulationComponent.CurrentWorld.LoadOrCreateRoom(new Point(x, y) + Game.SimulationComponent.CurrentRoomCoordinate);
                renderers[i].ReloadChunk(room);
            }
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

            

            if (Game.CurrentGameState == GameState.Build)
            {
                effect.CurrentTechnique = effect.Techniques["Build"];
                effect.Parameters["CurrentLayer"].SetValue(Game.CurrentLayer);
            }
            else if (Game.CurrentGameState == GameState.Debug || Game.CurrentGameState == GameState.Running)
            {
                effect.CurrentTechnique = effect.Techniques["Run"];
                
                effect.Parameters["AmbientColor"].SetValue(Color.White);
                effect.Parameters["AmbientIntensity"].SetValue(0.2f);
                effect.Parameters["LightPosition"].SetValue(new Vector2(7,8));
            }

            if (Game.CurrentGameState == GameState.Running)
            {
                renderers[4].Render(GraphicsDevice,effect,view,projection);
            }
            else
            {
                for (int i = 0; i < renderers.Length; i++)
                {
                    var renderer = renderers[i];
                    renderer.Render(GraphicsDevice,effect,view,projection);
                }   
            }
        }
    }
}