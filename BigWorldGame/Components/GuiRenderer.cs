using System;
using BigWorld;
using BigWorld.Map;
using BigWorldGame.Components.Gui;
using engenious;
using engenious.Graphics;
using engenious.Input;
using ButtonState = engenious.Input.ButtonState;
using Keyboard = engenious.Input.Keyboard;
using Mouse = engenious.Input.Mouse;
using BigWorldGame.Controls;

namespace BigWorldGame.Components
{
    public class GuiRenderer : DrawableGameComponent
    {
        public new readonly MainGame Game;

        public RootControl RootControl { get; private set; }
        
        public SpriteBatch SpriteBatch { get; private set; }

        public GuiRenderer(MainGame game) : base(game)
        {
            Game = game;
            RootControl = new RootControl();
            RootControl.ClientRectangle = new Rectangle(0,0,game.Window.ClientSize.Width, game.Window.ClientSize.Height);
            RootControl.BackgroundColor = Color.Green;
        }

        protected override void LoadContent()
        {
            RootControl.LoadContent(Game);

            var child = new Button("Demo") { ClientRectangle = new Rectangle(10, 10, 100, 100), BackgroundColor = Color.Red };
            child.LoadContent(Game);
            RootControl.Children.Add(child);


            SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }
        
        public override void Update(GameTime gameTime)
        {
            RootControl.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            RootControl.Draw(SpriteBatch, RootControl.ClientRectangle, 1);
            SpriteBatch.End();
        }
    }
}