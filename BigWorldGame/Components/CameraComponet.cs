using BigWorld.Map;
using engenious;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWorldGame.Components
{
    public class CameraComponet : DrawableGameComponent
    {
        public Matrix Projection { get; private set; }
        public Matrix View { get; private set; }

        public CameraComponet(Game game) : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            Projection = Matrix.CreateOrthographic(
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height, -1.1f, 10);

            var posX = 16 * Room.SizeX;
            var posY = 16 * Room.SizeY;

            View = Matrix.CreateLookAt(new Vector3(posX, posY, 10), new Vector3(posX, posY, 0), Vector3.UnitY)
                          * Matrix.CreateScaling(new Vector3(2));
        }
    }
}
