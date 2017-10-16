using BigWorldGame.Components;
using engenious;

namespace BigWorldGame
{
    public class MainGame : Game
    {
        public MainGame()
        {   
            RoomRenderer roomRenderer = new RoomRenderer(this);
            Components.Add(roomRenderer);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}