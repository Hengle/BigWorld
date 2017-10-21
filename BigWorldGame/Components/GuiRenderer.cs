using BigWorld.Map;
using BigWorldGame.Controlls;
using engenious;
using engenious.Graphics;
using engenious.Input;
using ButtonState = engenious.Input.ButtonState;
using Keyboard = engenious.Input.Keyboard;
using Mouse = engenious.Input.Mouse;

namespace BigWorldGame.Components
{
    public class GuiRenderer : DrawableGameComponent
    {
        public new readonly MainGame Game;

        private SpriteBatch batch;
        
        private Texture2D pixeltexture;
        private Texture2D dungeonSheet;

        private SpriteFont gameFont;
        
        private Color mouseColor = Color.Red * 0.3f;

        private SelectTileSheetControl tileSheetControl;
        
        private Point? mouseMapPoint = null;

        private int currentLayer = 0;
        private Trigger<bool> pageUpTrigger = new Trigger<bool>();
        private Trigger<bool> pageDownTrigger = new Trigger<bool>();
        
        public GuiRenderer(MainGame game) : base(game)
        {
            Game = game;
        }

        protected override void LoadContent()
        {
            batch = new SpriteBatch(GraphicsDevice);
            
            pixeltexture = new Texture2D(GraphicsDevice,1,1);
            pixeltexture.SetData(new Color[] {Color.White});
            
            dungeonSheet = Game.Content.Load<Texture2D>("Spritesheets/TileSheetDungeon");
            gameFont = Game.Content.Load<SpriteFont>("Fonts/Hud");
            
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
                var room = Game.CurrentWorld.LoadOrCreateRoom(Game.BasePoint);
                room[currentLayer].SetValue(mouseMapPoint.Value,tileSheetControl.SelectTextureInteger);
            }
            else if (mouseMapPoint.HasValue && mouseState.RightButton == ButtonState.Pressed)
            {
                var room = Game.CurrentWorld.LoadOrCreateRoom(Game.BasePoint);
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


            Game.CurrentLayer = currentLayer;
            tileSheetControl.Update();

        }

        public override void Draw(GameTime gameTime)
        {
            var height = (GraphicsDevice.Viewport.Height - Room.SizeY * RenderSettings.TileSize)/2;
            
            batch.Begin();

            if (mouseMapPoint.HasValue)
            {
                batch.Draw(pixeltexture,new Rectangle(mouseMapPoint.Value.X*RenderSettings.TileSize+height,mouseMapPoint.Value.Y*RenderSettings.TileSize+height,RenderSettings.TileSize,RenderSettings.TileSize),mouseColor );
            }
            
            
            batch.DrawString(gameFont,$"Layer:{currentLayer}",new Vector2(10,10),Color.White );
            

            batch.End();
            
            tileSheetControl.Draw();
        }
    }
}