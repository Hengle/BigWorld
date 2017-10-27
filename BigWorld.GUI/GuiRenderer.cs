using System;
using BigWorld;
using engenious;
using engenious.Graphics;
using engenious.Input;
using ButtonState = engenious.Input.ButtonState;
using Keyboard = engenious.Input.Keyboard;
using Mouse = engenious.Input.Mouse;

namespace BigWorld.GUI
{
    public class GuiRenderer : DrawableGameComponent
    {
        public new readonly Game Game;
        
        public SpriteBatch SpriteBatch { get; private set; }

        public RootControl RootControl { get; private set; }

        public GuiRenderer(Game game) : base(game)
        {
            Game = game;
            RootControl = new RootControl();

            StackPanel p = new StackPanel();
            p.ItemSpacing = 10;
            p.VerticalAlignment = Layout.VerticalAlignment.Top;
            p.Orientation = Layout.Orientation.Horizontal;
            
            var button1 = new Button("Start");
            p.Children.Add(button1);
            var button2 = new Button("Start");
            p.Children.Add(button2);
            var button3 = new Button("Start");
            p.Children.Add(button3);

            RootControl.Children.Add(p);
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            RootControl.LoadContent(Game);
        }
        
        public override void Update(GameTime gameTime)
        {
            RootControl.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            RootControl.Draw(SpriteBatch, Game.GraphicsDevice.Viewport.Bounds, 1);
        }
    }
}