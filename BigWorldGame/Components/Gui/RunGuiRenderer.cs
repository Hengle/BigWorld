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

            var maxFitness = Game.SimulationComponent.AiSimulation.MaxFitness;
            var run = Game.SimulationComponent.AiSimulation.RunCounter;
            
            batch.DrawString(font,run.ToString(),new Vector2(0,0),Color.White );
            /*
            batch.DrawString(font,count.ToString(),new Vector2(0,20),Color.White );
            batch.DrawString(font,genomeNumber.ToString(),new Vector2(0,40),Color.White );
            if (genome != null)
            {
                batch.DrawString(font,$"{genome.Generation}",new Vector2(0,60),Color.White );
            }
            */
            batch.DrawString(font,maxFitness.ToString(),new Vector2(0,80),Color.White );
            
            
            batch.End();
        }
    }
}