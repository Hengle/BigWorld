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

           // StackPanel stackPanel = new StackPanel();
           // stackPanel.ItemSpacing = 10;
           // stackPanel.VerticalAlignment = Layout.VerticalAlignment.Top;
           // stackPanel.Orientation = Layout.Orientation.Horizontal;
            
           // var button1 = new Button("Start");
           // stackPanel.Children.Add(button1);
           // var button2 = new Button("Start");
           // stackPanel.Children.Add(button2);
           //// var button3 = new Button("Start");
           //// stackPanel.Children.Add(button3);

            //var button4 = new Button("Demo");

            //var label = new Label() { Text = "Demo" };
            //label.HorizontalAlignment = Layout.HorizontalAlignment.Right;
            //label.BackgroundColor = Color.White * 0.5f;

            StackPanel stack = new StackPanel();
            stack.ItemSpacing = 10;
            stack.BackgroundColor = Color.Green;
            stack.Orientation = Layout.Orientation.Vertical;

            for(int i = 0; i < 10; i++)
            {
                stack.Children.Add(new Button("Demo " + i));
            }

            ScrollPanel sp = new ScrollPanel();
            sp.Content = stack;
            sp.HorizontalAlignment = Layout.HorizontalAlignment.Left;
            sp.Width = 400;
            sp.Height = 200;
            sp.BackgroundColor = Color.Red;
            sp.Children.Add(stack);
            RootControl.Children.Add(sp);

            Button scrollButton = new Button("Scroll");
            scrollButton.HorizontalAlignment = Layout.HorizontalAlignment.Right;
            scrollButton.ButtonClick += (s, e) => sp.ScrollPositionY += 5;
            RootControl.Children.Add(scrollButton);

            //RootControl.Children.Add(stackPanel);
        //    RootControl.Children.Add(button4);
        //    RootControl.Children.Add(label);
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