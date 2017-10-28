using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWorld.GUI.Layout
{
    public class Border
    {
        public int Top { get; set; }

        public int Bottom { get; set; }

        public int Left { get; set; }

        public int Right { get; set; }

        public int Horizontal => Left + Right;

        public int Vertical => Top + Bottom;

        public Border(int top, int right, int bottom, int left)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }

        public Border(int all)
        {
            Top = all;
            Right = all;
            Bottom = all;
            Left = all;
        }
    }
}
