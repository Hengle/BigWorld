using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigWorld.GUI.Layout
{
    public static class LayoutHelper
    {
        public static int CalculateXOffset(HorizontalAlignment alignment, ControlSize availableSize, ControlSize childSize)
        {
            var offsetX = 0;

            switch (alignment)
            {
                case Layout.HorizontalAlignment.Left:
                    break;
                case Layout.HorizontalAlignment.Right:
                    offsetX = (availableSize.Width ?? 0) - (childSize.Width ?? 0);
                    break;
                case Layout.HorizontalAlignment.Center:
                default:
                    offsetX = ((availableSize.Width ?? 0) - (childSize.Width ?? 0)) / 2;
                    break;
            }

            return offsetX;
        }

        public static int CalculateYOffset(VerticalAlignment alignment, ControlSize availableSize, ControlSize childSize)
        {
            var offsetY = 0;
            switch (alignment)
            {
                case Layout.VerticalAlignment.Top:
                    break;
                case Layout.VerticalAlignment.Bottom:
                    offsetY = (availableSize.Height ?? 0) - (childSize.Height ?? 0);
                    break;
                case Layout.VerticalAlignment.Center:
                default:
                    offsetY = ((availableSize.Height ?? 0) - childSize.Height ?? 0) / 2;
                    break;
            }
            return offsetY;
        }
    }
}
