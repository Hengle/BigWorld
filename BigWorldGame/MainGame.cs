using BigWorld.Map;
using BigWorldGame.Components;
using engenious;
using engenious.Graphics;
using engenious.Input;

namespace BigWorldGame
{
    public class MainGame : Game
    {
        public readonly World CurrentWorld = new World();
        public int CurrentLayer = -1;
        
        public Point BasePoint;
        
        public MainGame()
        {   
            mapRenderer = new MapRenderer(this);
            Components.Add(mapRenderer);
            
            guiRenderer = new GuiRenderer(this);
            Components.Add(guiRenderer);
        }

        

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            //GraphicsDevice.Clear(Color.White);

            GraphicsDevice.Viewport = new Viewport(0,0,Window.ClientSize.Height,Window.ClientSize.Height);
            mapRenderer.Draw(gameTime);
            
            GraphicsDevice.Viewport = new Viewport(0,0,Window.ClientSize.Width,Window.ClientSize.Height);
            guiRenderer.Draw(gameTime);
            
        }
        
        private Trigger<bool> upTrigger = new Trigger<bool>();
        private Trigger<bool> downTrigger = new Trigger<bool>();
        
        private Trigger<bool> leftTrigger = new Trigger<bool>();
        private Trigger<bool> rightTrigger = new Trigger<bool>();
        private MapRenderer mapRenderer;
        private GuiRenderer guiRenderer;

        public override void Update(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();

            if (upTrigger.IsChanged(keyState.IsKeyDown(Keys.W),k => k))
            {
                BasePoint = new Point(BasePoint.X,BasePoint.Y-1);
            }
            if (downTrigger.IsChanged(keyState.IsKeyDown(Keys.S),k => k))
            {
                BasePoint = new Point(BasePoint.X,BasePoint.Y+1);
            }
            
            if (leftTrigger.IsChanged(keyState.IsKeyDown(Keys.A),k => k))
            {
                BasePoint = new Point(BasePoint.X-1,BasePoint.Y);
            }
            
            if (rightTrigger.IsChanged(keyState.IsKeyDown(Keys.D),k => k))
            {
                BasePoint = new Point(BasePoint.X+1,BasePoint.Y);
            }
            
            base.Update(gameTime);
        }
    }
}