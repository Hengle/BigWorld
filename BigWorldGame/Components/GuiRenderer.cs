using System.Runtime.InteropServices.ComTypes;
using BigWorld.Map;
using BigWorldGame.Controlls;
using engenious;
using engenious.Graphics;
using engenious.Input;
using OpenTK.Graphics.ES30;

namespace BigWorldGame.Components
{
    public class GuiRenderer : DrawableGameComponent
    {
        public new readonly MainGame Game;

        private SpriteBatch batch;
        
        private Texture2D pixeltexture;
        private Texture2D dungeonSheet; 
        
        private Color mouseColor = Color.Red * 0.3f;

        private SelectTileSheetControl tileSheetControl;
        
        private Point? mouseMapPoint = null;
        
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
                room.TileLayer[0].SetValue(mouseMapPoint.Value,tileSheetControl.SelectTextureInteger);
            }
            else if (mouseMapPoint.HasValue && mouseState.RightButton == ButtonState.Pressed)
            {
                var room = Game.CurrentWorld.LoadOrCreateRoom(Game.BasePoint);
                room.TileLayer[0].SetValue(mouseMapPoint.Value,null);
            }
            

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

            
            
            batch.End();
            
            tileSheetControl.Draw();
        }
    }
}