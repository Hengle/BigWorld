using System.Linq.Expressions;
using BigWorld.Map;
using BigWorldGame.Controls;
using engenious;
using engenious.Graphics;
using engenious.Input;

namespace BigWorldGame.Components.Gui
{
    public class BuildGuiRenderer : DrawableGameComponent
    {
        public new readonly MainGame Game;
        
        private SelectTileSheetControl tileSheetControl;
        
        private Texture2D dungeonSheet;
        private Texture2D pixelTexture;
        private SpriteFont gameFont;
        
        private SpriteBatch batch;
        
        private readonly Color mouseColor = Color.Red * 0.3f;
        private Point? mouseMapPoint = null;
        
        private int currentLayer = 0;
        
        private readonly Trigger<bool> pageUpTrigger = new Trigger<bool>();
        private readonly Trigger<bool> pageDownTrigger = new Trigger<bool>();

        private readonly Trigger<bool> groundFill = new Trigger<bool>();
        
        public BuildGuiRenderer(MainGame game) : base(game)
        {
            Game = game;
        }

        public new void LoadContent()
        {
            batch = new SpriteBatch(GraphicsDevice);
            
            dungeonSheet = Game.Content.Load<Texture2D>("Spritesheets/TileSheetDungeon");
            
            pixelTexture = new Texture2D(GraphicsDevice,1,1);
            pixelTexture.SetData(new []{Color.White});

            gameFont = Game.Content.Load<SpriteFont>("Fonts/GameFont");
            
            tileSheetControl= new SelectTileSheetControl();
            tileSheetControl.LoadContent(Game);
            tileSheetControl.SetTileSheet(dungeonSheet);
            tileSheetControl.Position = new Rectangle(GraphicsDevice.Viewport.Width-300,10,300,GraphicsDevice.Viewport.Height);
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            
            var height = (GraphicsDevice.Viewport.Height - Room.SizeY * RenderSettings.TileSize)/2;
            
            
            mouseMapPoint = new Point((mouseState.X -height)/RenderSettings.TileSize, (mouseState.Y -height)/RenderSettings.TileSize);

            if (mouseMapPoint.Value.X < 0 || mouseMapPoint.Value.Y < 0)
            {
                mouseMapPoint = null;
            }
            else if(mouseMapPoint.Value.X >= Room.SizeX || mouseMapPoint.Value.Y >= Room.SizeY)
            {
                mouseMapPoint = null;
            }

            if (mouseMapPoint.HasValue && mouseState.LeftButton == ButtonState.Pressed)
            {
                var room = Game.SimulationComponent.CurrentWorld.LoadOrCreateRoom(Game.SimulationComponent.CurrentRoomCoordinate);
                room[currentLayer].SetValue(mouseMapPoint.Value,tileSheetControl.SelectTextureInteger);
            }
            else if (mouseMapPoint.HasValue && mouseState.RightButton == ButtonState.Pressed)
            {
                var room = Game.SimulationComponent.CurrentWorld.LoadOrCreateRoom(Game.SimulationComponent.CurrentRoomCoordinate);
                room[currentLayer].SetValue(mouseMapPoint.Value,null);
            }
            
            
            //Keyboard
            var keyState = Keyboard.GetState();

            if (pageUpTrigger.IsChanged(keyState.IsKeyDown(Keys.PageUp),i => i))
            {
                currentLayer++;
            }
            else if (pageDownTrigger.IsChanged(keyState.IsKeyDown(Keys.PageDown),i => i))
            {
                currentLayer--;
                if (currentLayer < 0)
                {
                    currentLayer = 0;
                }
            }

            if (groundFill.IsChanged((keyState.IsKeyDown(Keys.F)),i => i))
            {
                var room = Game.SimulationComponent.CurrentWorld.LoadOrCreateRoom(Game.SimulationComponent.CurrentRoomCoordinate);
                
                for (int x = 0; x < Room.SizeX; x++)
                {
                    for (int y = 0; y < Room.SizeY; y++)
                    {
                        room[0].SetValue(new Point(x,y), tileSheetControl.SelectTextureInteger);
                    }
                    
                }
            }
            

            Game.CurrentLayer = currentLayer;
            tileSheetControl.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            batch.Begin();
            
            
            var height = (GraphicsDevice.Viewport.Height - Room.SizeY * RenderSettings.TileSize)/2;
            if (mouseMapPoint.HasValue)
            {
                batch.Draw(pixelTexture,new Rectangle(mouseMapPoint.Value.X*RenderSettings.TileSize+height,mouseMapPoint.Value.Y*RenderSettings.TileSize+height,RenderSettings.TileSize,RenderSettings.TileSize),mouseColor );
            }
                
            batch.DrawString(gameFont,$"Layer:{currentLayer}",new Vector2(10,10),Color.White );
            tileSheetControl.Draw();

            batch.End();
        }
    }
}