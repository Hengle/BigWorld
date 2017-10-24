using engenious;

namespace BigWorldGame.Components.Gui
{
    public class DebugGuiRenderer : DrawableGameComponent
    {
        public readonly MainGame Game;
        
        public DebugGuiRenderer(MainGame game) : base(game)
        {
            Game = game;
        }

        public new void LoadContent()
        {
            base.LoadContent();
        }
    }
}