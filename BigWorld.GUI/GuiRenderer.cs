using BigWorld.GUI.Brushes;
using BigWorld.GUI.Controls;
using engenious;
using engenious.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWorld.GUI
{
    public class GuiRenderer : DrawableGameComponent
    {
        public static Texture2D Pixel { get; private set; }

        public RootControl RootControl { get; private set; }

        private SpriteBatch spriteBatch;

        public GuiRenderer(Game game) : base(game)
        {
            RootControl = new RootControl();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            RootControl.LoadContent(Game);

            spriteBatch = new SpriteBatch(GraphicsDevice);

            Pixel = new Texture2D(GraphicsDevice, 1, 1);
            Pixel.SetData(new Color[] { Color.White });
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            RootControl.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            RootControl.Height = GraphicsDevice.Viewport.Bounds.Height;
            RootControl.Width = GraphicsDevice.Viewport.Bounds.Width;
            RootControl.Draw(spriteBatch, Matrix.Identity, GraphicsDevice.Viewport.Bounds, GraphicsDevice.Viewport.Bounds.Size, gameTime);
        }
    }
}