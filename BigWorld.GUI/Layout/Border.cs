using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWorld.GUI.Layout
{
    public class Border
    {
        public int Left { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
        public int Top { get; set; }

        public int Vertical { get => Top + Bottom; }
        public int Horizontal { get => Left + Right; }

        public Border(int top, int right, int bottom, int left)
        {
            Top = top;
            Left = left;
            Right = right;
            Bottom = bottom;
        }

        public Border(int all)
        {
            Top = all;
            Left = all;
            Right = all;
            Bottom = all;
        }
    }
}
