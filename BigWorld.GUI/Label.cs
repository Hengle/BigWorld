using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;

namespace BigWorld.GUI
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

        protected override void OnDraw(SpriteBatch batch, Rectangle destinationRectangle, float alpha)
        {
            base.OnDraw(batch, destinationRectangle, alpha);

            if (Font != null && !String.IsNullOrEmpty(Text))
            {
                var textSize = Font.MeasureString(Text);
                batch.DrawString(Font, Text, new Vector2((destinationRectangle.Width - textSize.X - Padding.Horizontal) / 2,
                    (destinationRectangle.Height - textSize.Y - Padding.Vertical) / 2), Color.White);
            }
        }
    }
}
