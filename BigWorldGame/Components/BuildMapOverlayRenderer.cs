using engenious;
using engenious.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWorldGame.Components
{
    public class BuildMapOverlayRenderer : DrawableGameComponent
    {
        private SpriteBatch batch;
        private Texture2D pixel;

        public BuildMapOverlayRenderer(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            batch = new SpriteBatch(GraphicsDevice);

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
        }

        public override void Draw(GameTime gameTime)
        {
            batch.Begin();

            batch.End();
        }
    }
}
