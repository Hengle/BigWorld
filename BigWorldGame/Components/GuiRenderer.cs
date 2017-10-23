using BigWorld;
using BigWorld.Map;
using BigWorldGame.Components.Gui;
using BigWorldGame.Controlls;
using engenious;
using engenious.Graphics;
using engenious.Input;
using ButtonState = engenious.Input.ButtonState;
using Keyboard = engenious.Input.Keyboard;
using Mouse = engenious.Input.Mouse;

namespace BigWorldGame.Components
{
    public class GuiRenderer : DrawableGameComponent
    {
        public new readonly MainGame Game;

        private BuildGuiRenderer buildGuiRenderer;
        
        public GuiRenderer(MainGame game) : base(game)
        {
            Game = game;
        }

        protected override void LoadContent()
        {        
            buildGuiRenderer = new BuildGuiRenderer(Game);
            buildGuiRenderer.LoadContent();
        }

        
        
        
        public override void Update(GameTime gameTime)
        {
            if (Game.CurrentGameState == GameState.Build)
            {
                buildGuiRenderer.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (Game.CurrentGameState == GameState.Build)
            {
                buildGuiRenderer.Draw(gameTime);
            }
        }
    }
}