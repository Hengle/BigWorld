using System.Threading;
using BigWorld.World;
using engenious;
using engenious.Graphics;

namespace BigWorldGame.Components
{
    public class RoomRenderer : DrawableGameComponent
    {
        public new readonly MainGame Game;

        private SpriteBatch spriteBatch;
        private Texture2D pixeltexture;
        
        
        public RoomRenderer(MainGame game) : base(game)
        {
            this.Game = game;
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pixeltexture = new Texture2D(GraphicsDevice,1,1);
            pixeltexture.SetData(new Color[] {Color.White});
        }

        public override void Draw(GameTime gameTime)
        {   
            spriteBatch.Begin();

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    DrawRoom(new Point(x,y));
                }  
            }
            
            spriteBatch.End();
        }

        private void DrawRoom(Point point)
        {
            var height = (GraphicsDevice.Viewport.Height - Room.RoomSizeY * 16)/2;
            
            var size = new Point(16,16);
            
            var delta = new Point(size.X * Room.RoomSizeX* point.X,size.Y * Room.RoomSizeY* point.Y);

            float alpha = point.IsEmpty ? 1f : 0.5f; 
            
            for (int x = 0; x < Room.RoomSizeX; x++)
            {
                for (int y = 0; y < Room.RoomSizeY; y++)
                {
                    var drawPoint = new Point(x*16+height,y*16+height) + delta;
                    
                    
                    spriteBatch.Draw(pixeltexture,new Rectangle(drawPoint,size),Color.White * alpha );
                }
            }
        }
    }
}