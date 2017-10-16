using BigWorld.Map;
using BigWorldGame.Components;
using engenious;
using engenious.Input;

namespace BigWorldGame
{
    public class MainGame : Game
    {
        public readonly World CurrentWorld = new World();

        public Point BasePoint;
        
        public MainGame()
        {   
            RoomRenderer roomRenderer = new RoomRenderer(this);
            Components.Add(roomRenderer);
            
            GuiRenderer guiRenderer = new GuiRenderer(this);
            Components.Add(guiRenderer);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
        
        private Trigger<bool> upTrigger = new Trigger<bool>();
        private Trigger<bool> downTrigger = new Trigger<bool>();
        
        private Trigger<bool> leftTrigger = new Trigger<bool>();
        private Trigger<bool> rightTrigger = new Trigger<bool>(); 
        
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