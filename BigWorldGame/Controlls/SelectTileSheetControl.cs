using engenious;
using engenious.Graphics;
using engenious.Input;

namespace BigWorldGame.Controlls
{
    public class SelectTileSheetControl : BaseControl
    {

        private Texture2D tilesheet;
        private Texture2D pixel;
        
        private SpriteBatch batch;

        public int ScrollPosition = 0;
        public int DrawSize = 32;
        public int DrawMargin = 2;
        
        public Point SelectedTexture = Point.Zero;
        public uint SelectTextureInteger = 0;
        
        public Point? HooverTexture = null;
        public uint? HooverTextureInteger = null;
        
        private int oldWheelPosition = 0;

        private int columns = 0;

        private Trigger<bool> leftSelectMouseTrigger = new Trigger<bool>();
        
        public void SetTileSheet(Texture2D tilesheet)
        {
            this.tilesheet = tilesheet;
        }

        public override void LoadContent(Game game)
        {
            batch = new SpriteBatch(game.GraphicsDevice);
            
            pixel = new Texture2D(game.GraphicsDevice,1,1);
            pixel.SetData(new [] {Color.White});
        }

        public override void Update()
        {
            var mouseState = Mouse.GetState();

            columns = Position.Width / (DrawSize + 2 * DrawMargin);

            if (!Position.Contains(mouseState.X, mouseState.Y))
            {
                HooverTexture = null;
                HooverTextureInteger = null;
                return;
            }
            
            //HoverPosition
            
            var mouseRelativPosition = new Point(mouseState.X,mouseState.Y) - Position.Location;
            var hooverRow = mouseRelativPosition.Y / (DrawSize + 2 * DrawMargin) + ScrollPosition;
            var hooverColumn = mouseRelativPosition.X / (DrawSize + 2 * DrawMargin);

            var hooverTilePosition = hooverRow * columns + hooverColumn;
            var texSizeX = (tilesheet.Width + 1) / 17;
            var texSizeY = (tilesheet.Height + 1) / 17f;
            
            var texX = hooverTilePosition % texSizeX;
            var texY = hooverTilePosition / texSizeX;

            HooverTextureInteger = (uint)hooverTilePosition;
            HooverTexture = new Point(texX,texY);

            if (leftSelectMouseTrigger.IsChanged(mouseState.LeftButton == ButtonState.Pressed,i => i))
            {
                SelectedTexture = HooverTexture.Value;
                SelectTextureInteger = HooverTextureInteger.Value;
            }
            
            //Scoll View
            var wheelValue = mouseState.ScrollWheelValue;

            if (wheelValue != oldWheelPosition)
            {
                var diff = wheelValue - oldWheelPosition;

                ScrollPosition += diff;

                if (ScrollPosition < 0)
                {
                    ScrollPosition = 0;
                }
                //Todo:
                else if (ScrollPosition > 100)
                {
                    ScrollPosition = 100;
                }
            }

            oldWheelPosition = wheelValue;
        }

        public override void Draw()
        {
            if(tilesheet == null)
                return;
            
            batch.Begin();

            batch.Draw(pixel,Position,Color.Azure);
            
            var texSizeX = (tilesheet.Width + 1) / 17;
            var texSizeY = (tilesheet.Height + 1) / 17f;
            
            var tiles = texSizeX  * texSizeY ;
            
            
            for (int i = 0; i < tiles; i++)
            {
                var x = i % columns;
                var y = i / columns - ScrollPosition;

                if (y < 0)
                    continue;
                
                
                var texX = i % texSizeX;
                var texY = i / texSizeX;

                
                
                
                var destRec = new Rectangle(x * (DrawSize) + Position.X + (2*x+1) *DrawMargin
                    ,y * (DrawSize) + Position.Y + (2*y+1) * DrawMargin 
                    , DrawSize, DrawSize);
                
                var sourceRec = new Rectangle(texX * 17, texY * 17, 16, 16);
                
                
                batch.Draw(tilesheet,destRec, sourceRec ,Color.White);
                
                if (texX == SelectedTexture.X && texY == SelectedTexture.Y)
                {
                    batch.Draw(pixel,destRec,Color.Red * 0.5f);
                }

                if (HooverTexture.HasValue && texX == HooverTexture.Value.X && texY == HooverTexture.Value.Y)
                {
                    batch.Draw(pixel,destRec,Color.CornflowerBlue * 0.5f);
                }
                
                

                
                
                
            }
            
            batch.End();
        }
    }
}