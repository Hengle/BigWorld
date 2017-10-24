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

namespace BigWorldGame.Components
{
    public class GuiRenderer : DrawableGameComponent
    {
        public new readonly MainGame Game;

        private BuildGuiRenderer buildGuiRenderer;
        private DebugGuiRenderer debugGuiRenderer;
        
        public GuiRenderer(MainGame game) : base(game)
        {
            Game = game;
        }

        protected override void LoadContent()
        {        
            buildGuiRenderer = new BuildGuiRenderer(Game);
            buildGuiRenderer.LoadContent();
            
            debugGuiRenderer = new DebugGuiRenderer(Game);
            debugGuiRenderer.LoadContent();
        }

        
        
        
        public override void Update(GameTime gameTime)
        {
            switch(Game.CurrentGameState)
                {
                    case GameState.Build:
                        buildGuiRenderer.Update(gameTime);
                        break;
                    case GameState.Debug:
                        debugGuiRenderer.Update(gameTime);
                        break;
                    case GameState.Running:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
        }

        public override void Draw(GameTime gameTime)
        {
            switch (Game.CurrentGameState)
            {
                case GameState.Build:
                    buildGuiRenderer.Draw(gameTime);
                    break;
                case GameState.Debug:
                    debugGuiRenderer.Draw(gameTime);
                    break;
                case GameState.Running:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}