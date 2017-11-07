using engenious;
using engenious.Graphics;

namespace BigWorldGame.Components.Gui
{
    public class RunGuiRenderer : DrawableGameComponent
    {
        private SpriteBatch batch;
        private SpriteFont font;

        public new MainGame Game;
        
        public RunGuiRenderer(MainGame game) : base(game)
        {
            Game = game;
        }
        
        public new void LoadContent()
        {
            batch = new SpriteBatch(GraphicsDevice);
            font = Game.Content.Load<SpriteFont>("Fonts/GameFont");
        }

        public override void Draw(GameTime gameTime)
        {
            batch.Begin();

            var run = Game.SimulationComponent.Simulation.NeuronalSimulationService.Run;
            var count = Game.SimulationComponent.Simulation.NeuronalSimulationService.Count;
            var genomeNumber = Game.SimulationComponent.Simulation.NeuronalSimulationService.CurrentGenomeNumber;
            var genome = Game.SimulationComponent.Simulation.NeuronalSimulationService.CurrentGenome;
            
            batch.DrawString(font,run.ToString(),new Vector2(0,0),Color.White );
            batch.DrawString(font,count.ToString(),new Vector2(0,20),Color.White );
            batch.DrawString(font,genomeNumber.ToString(),new Vector2(0,40),Color.White );
            if (genome != null)
            {
                batch.DrawString(font,$"{genome.Generation}",new Vector2(0,60),Color.White );
            }

            
            batch.End();
        }
    }
}