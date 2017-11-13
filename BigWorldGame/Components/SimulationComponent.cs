using System;
using System.Threading.Tasks;
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
       
        public readonly AISimulation AiSimulation = new AISimulation();
        
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
                Player = Simulation.AddAIPlayer();

                Task.Run((Action)CreateKI);
            }
        }

        private void CreateKI()
        {
            
            var genome = AiSimulation.Run(10000,CurrentWorldMap);
            
            Player.NeuronalNetwork.MapInputOutput(genome);
            Player.NeuronalNetwork.Reset(genome);
        }

        private double oldMaxFitness = 0;
        
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

            if (oldMaxFitness != AiSimulation.MaxFitness)
            {
                oldMaxFitness = AiSimulation.MaxFitness;

                var genome = AiSimulation.BestGenome;
                
                Player.NeuronalNetwork.MapInputOutput(genome);
                Player.NeuronalNetwork.Reset(genome);
            }
            
            Simulation.Update(gameTime);
        }
    }
}