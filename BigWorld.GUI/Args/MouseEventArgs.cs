using engenious;
using engenious.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWorld.GUI.Args
{
    public class MouseEventArgs
    {
        public MouseState MouseState { get; private set; }
        public int X => MouseState.X;
        public int Y => MouseState.Y;

        public Point Position { get; private set; }

        public MouseEventArgs(MouseState state)
        {
            MouseState = state;
            Position = new Point(X, Y);
        }

        public Point GetRelativePosition(Rectangle rectangle)
        {
            return new Point(X - rectangle.X, Y - rectangle.Y);
        }
    }
}
