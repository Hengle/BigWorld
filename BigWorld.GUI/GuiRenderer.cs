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
            RootControl.BackgroundColor = Color.CornflowerBlue;

            var stackPanel = new StackPanel();
            stackPanel.HorizontalAlignment = Layout.HorizontalAlignment.Stretch;
            stackPanel.Position = new Point(20, 20);
            stackPanel.BackgroundColor = Color.Bisque;
            stackPanel.ItemSpacing = 10;

            var content = new ContentControl();
            content.BackgroundColor = Color.DarkSeaGreen;
            content.HorizontalAlignment = Layout.HorizontalAlignment.Stretch;
            content.Height = 200;
            //content.Position = new Point(100, 100);
            stackPanel.Children.Add(content);


            var nestedContent = new ContentControl();
            nestedContent.BackgroundColor = Color.IndianRed;
            nestedContent.Height = 200;
            nestedContent.Width = 400;
            nestedContent.HorizontalAlignment = Layout.HorizontalAlignment.Right;
            //nestedContent.Position = new Point(30, 30);
            stackPanel.Children.Add(nestedContent);

            RootControl.Content = stackPanel;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            Pixel = new Texture2D(GraphicsDevice, 1, 1);
            Pixel.SetData(new Color[] { Color.White });
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            RootControl.Height = GraphicsDevice.Viewport.Bounds.Height;
            RootControl.Width = GraphicsDevice.Viewport.Bounds.Width;
            RootControl.Draw(spriteBatch, Matrix.Identity, GraphicsDevice.Viewport.Bounds, gameTime);
        }
    }
}