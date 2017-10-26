using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;

namespace BigWorld.GUI.Controls
{
    public class Label : Control
    {
        public string Text { get; set; } = "";

        public SpriteFont Font = null;

        public override void LoadContent(Game game)
        {
            base.LoadContent(game);

            if (Font == null)
                Font = game.Content.Load<SpriteFont>("Fonts\\GameFont");
        }

        public override void Draw(SpriteBatch batch, Rectangle destinationRectangle, float alpha)
        {
            base.Draw(batch, destinationRectangle, alpha);

            if (Font != null && !String.IsNullOrEmpty(Text))
                batch.DrawString(Font, Text, ClientRectangle.Location.ToVector2(), Color.White);
        }
    }
}
