using System.Runtime.InteropServices.ComTypes;
using BigWorld.Map;
using engenious;
using engenious.Graphics;
using engenious.Input;

namespace BigWorldGame.Components
{
    public class GuiRenderer : DrawableGameComponent
    {
        public new readonly MainGame Game;

        private SpriteBatch batch;
        
        private Texture2D pixeltexture;
        
        private Color mouseColor = Color.Red * 0.3f; 
        
        public GuiRenderer(MainGame game) : base(game)
        {
            Game = game;
        }

        protected override void LoadContent()
        {
            batch = new SpriteBatch(GraphicsDevice);
            
            pixeltexture = new Texture2D(GraphicsDevice,1,1);
            pixeltexture.SetData(new Color[] {Color.White});
        }

        private Point? mouseMapPoint = null;
        
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
                room.TileLayer[0].SetValue(mouseMapPoint.Value,new Point(9,2));
            }
            
            
            
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
        }
    }
}