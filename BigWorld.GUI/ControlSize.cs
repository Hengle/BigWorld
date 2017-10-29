using engenious;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWorld.GUI
{
    public struct ControlSize
    {
        public int? Height { get; set; }

        public int? Width { get; set; }

        public ControlSize(int? width, int? height)
        {
            Height = height;
            Width = width;
        }

        public ControlSize(Size size)
        {
            Height = size.Height;
            Width = size.Width;
        }

        public Size ToSize()
        {
            return new Size(Width ?? 0, Height ?? 0);
        }

        public static implicit operator ControlSize (Size size)
        {
            return new ControlSize(size.Width, size.Height);
        }

        public static implicit operator Size(ControlSize size)
        {
            return new Size(size.Width ?? 0, size.Height ?? 0);
        }
    }
}
