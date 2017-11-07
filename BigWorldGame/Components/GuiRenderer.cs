﻿using System;
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

        public readonly BuildGuiRenderer BuildGuiRenderer;
        public readonly RunGuiRenderer RunGuiRenderer;
        
        public GuiRenderer(MainGame game) : base(game)
        {
            Game = game;
            
            BuildGuiRenderer = new BuildGuiRenderer(Game);
            RunGuiRenderer = new RunGuiRenderer(game);
        }

        protected override void LoadContent()
        {        
            BuildGuiRenderer.LoadContent();
            RunGuiRenderer.LoadContent();
        }

        
        
        
        public override void Update(GameTime gameTime)
        {
            switch(Game.CurrentGameState)
                {
                    case GameState.Debug:
                    case GameState.Build:
                        BuildGuiRenderer.Update(gameTime);
                        break;
                    case GameState.Running:
                        RunGuiRenderer.Update(gameTime);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
        }

        public override void Draw(GameTime gameTime)
        {
            switch (Game.CurrentGameState)
            {
                case GameState.Debug:
                case GameState.Build:
                    BuildGuiRenderer.Draw(gameTime);
                    break;
                case GameState.Running:
                    RunGuiRenderer.Draw(gameTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}