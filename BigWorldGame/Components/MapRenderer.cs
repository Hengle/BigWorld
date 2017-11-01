using System.Security.Cryptography.X509Certificates;
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
        
        private Point roomPoint;
        private WorldMap map;

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
            roomPoint = new Point(0, 0);
            map = null;
            
            if (Game.CurrentGameState == GameState.Build || Game.CurrentGameState == GameState.Debug)
            {
                roomPoint = Game.GuiRenderer.BuildGuiRenderer.CurrentRoomCoordinate;
                map = Game.BuildWorldMap;
            }
            else if (Game.CurrentGameState == GameState.Running)
            {
                roomPoint = Game.SimulationComponent.Player.Position.CurrentRoom;
                map = Game.SimulationComponent.CurrentWorldMap;
            }

            if (map == null)
                return;
            
            for (int i = 0; i < renderers.Length; i++)
            {
                var x = i % 3 - 1;
                var y = i / 3 - 1;

                var point = new Point(x, y) + roomPoint;
                
                Room room;
                if (!map.TryGetRoom(point,out room) 
                    && (Game.CurrentGameState == GameState.Build || Game.CurrentGameState == GameState.Debug) )
                {
                    room = map.LoadOrCreateRoom(point);
                }
                
                
                
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
                
                Room room;
                if (map.TryGetRoom(roomPoint,out  room))
                {
                    effect.Parameters["AmbientIntensity"].SetValue(room.AmbientIntensity);
                    effect.Parameters["AmbientColor"].SetValue(room.AmbientColor);
                    
                    for (int i = 0; i < Room.MaxRoomLights; i++)
                    {
                        var light = room.RoomLights[i];
                        
                        effect.Parameters[$"Lights[{i}].color"].SetValue(light.Color);
                        effect.Parameters[$"Lights[{i}].position"].SetValue(light.Position);
                        effect.Parameters[$"Lights[{i}].radius"].SetValue(light.Radius);
                        effect.Parameters[$"Lights[{i}].enable"].SetValue(light.Enable); 
                    }
                }
                
                
                

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