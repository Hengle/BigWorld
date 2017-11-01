using System;
using BigWorld;
using BigWorld.Map;
using engenious;
using engenious.Input;

namespace BigWorldGame.Components
{
    public class SimulationComponent : GameComponent
    {
        public new MainGame Game;       
        
        public readonly Simulation Simulation = new Simulation();
        public WorldMap CurrentWorldMap => Simulation.CurrentWorldMap;        

        public Player Player { get; private set; }

        public WorldMap SimulationWorld { get; set; }
       
        
        public SimulationComponent(MainGame game) : base(game)
        {
            Game = game;
        }

        public void Reset(GameState state)
        {
            if (state == GameState.Build || state == GameState.Debug)
            {
                Simulation.Stop();
            }
            else if (state == GameState.Running)
            {
                Simulation.Start(SimulationWorld);
                Player = Simulation.AddPlayer();
            }
        }
        
        public override void Update(GameTime gameTime)
        {
            if (!Simulation.IsRunning)
                return;
            
            
            var keyState = Keyboard.GetState();

            Vector2 direction = Vector2.Zero;

            if (keyState.IsKeyDown(Keys.A))
                direction += new Vector2(-1,0);
                
            if (keyState.IsKeyDown(Keys.D))
                direction += new Vector2(1,0);
                
            if (keyState.IsKeyDown(Keys.W))
                direction += new Vector2(0,-1);
                
            if (keyState.IsKeyDown(Keys.S))
                direction += new Vector2(0,1);

            Player.Input.MoveDirection = direction;
            
            Simulation.Update(gameTime);
        }
    }
}