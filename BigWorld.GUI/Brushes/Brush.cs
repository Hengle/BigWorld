using engenious;
using engenious.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWorld.GUI.Brushes
{
    public abstract class Brush
    {
        public virtual void Draw(SpriteBatch batch, Size size) { }
    }
}
