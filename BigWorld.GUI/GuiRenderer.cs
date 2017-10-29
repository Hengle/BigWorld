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

            var container = new ContainerControl();
            container.HorizontalAlignment = Layout.HorizontalAlignment.Stretch;
            container.VerticalAlignment = Layout.VerticalAlignment.Stretch;

            var scrollPanel = new ScrollContainer();
            scrollPanel.HorizontalAlignment = Layout.HorizontalAlignment.Left;
            scrollPanel.VerticalAlignment = Layout.VerticalAlignment.Center;
            scrollPanel.Width = 200;
            scrollPanel.Height = 300;
            scrollPanel.BackgroundColor = Color.PaleVioletRed;
            container.Children.Add(scrollPanel);

            var buttonStack = new StackPanel();
            buttonStack.BackgroundColor = Color.Green;
            buttonStack.Width = 180;
            buttonStack.ItemSpacing = 10;
            scrollPanel.Content = buttonStack;

            for(int i = 0; i < 10; i++)
            {
                buttonStack.Children.Add(new Button("Demo" + i)
                {
                    Width = 100,
                    Height = 50,
                    BackgroundColor = Color.Red
                });
            }

            var scrollButton = new Button("Scroll");
            scrollButton.HorizontalAlignment = Layout.HorizontalAlignment.Right;
            scrollButton.OnClick += (s, e) => scrollPanel.ScrollPositionX += 10;
            container.Children.Add(scrollButton);

            RootControl.Content = container;
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
            RootControl.Draw(spriteBatch, Matrix.Identity, GraphicsDevice.Viewport.Bounds, gameTime);
        }
    }
}