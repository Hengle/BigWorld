using BigWorld;
using BigWorld.Map;
using BigWorldGame.Components;
using engenious;
using engenious.Graphics;
using engenious.Input;

namespace BigWorldGame
{
    public class MainGame : Game
    {
        
        public int CurrentLayer = -1;
        
        

        public GameState CurrentGameState = GameState.Build;
                
        private readonly Trigger<bool> debugTrigger = new Trigger<bool>();
        private Trigger<bool> buildTrigger = new Trigger<bool>();
        
        public readonly MapRenderer MapRenderer;
        public readonly GuiRenderer GuiRenderer;
        public readonly SimulationComponent SimulationComponent;
        
        public MainGame()
        {   
            MapRenderer = new MapRenderer(this);
            Components.Add(MapRenderer);
            
            GuiRenderer = new GuiRenderer(this);
            Components.Add(GuiRenderer);
            
            SimulationComponent = new SimulationComponent(this);
            Components.Add(SimulationComponent);
        }

        

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            GraphicsDevice.Viewport = new Viewport(0,0,Window.ClientSize.Height,Window.ClientSize.Height);
            MapRenderer.Draw(gameTime);
            
            GraphicsDevice.Viewport = new Viewport(0,0,Window.ClientSize.Width,Window.ClientSize.Height);
            GuiRenderer.Draw(gameTime);
            
        }

        public override void Update(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();
            
            if (buildTrigger.IsChanged(keyState.IsKeyDown(Keys.F4), k => k))
            {
                CurrentGameState = GameState.Build;
            }
            
            if (debugTrigger.IsChanged(keyState.IsKeyDown(Keys.F5), k => k))
            {
                CurrentGameState = GameState.Debug;
            }
            
            base.Update(gameTime);
        }
    }
}