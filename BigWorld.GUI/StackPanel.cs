using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using BigWorld.GUI.Layout;

namespace BigWorld.GUI
{
    public class StackPanel : Control
    {
        public Orientation Orientation { get => orientation;
            set
            {
                if (orientation == value)
                    return;

                orientation = value;
                Invalidate();
            }
        }

        private Orientation orientation;

        public int ItemSpacing
        {
            get => itemSpacing;
            set
            {
                if (itemSpacing == value)
                    return;

                itemSpacing = value;
                Invalidate();
            }
        }
        private int itemSpacing = 0;

        public override void PerformLayout()
        {
            var contentArea = new Rectangle(ActualClientRectangle.X + Padding.Left, ActualClientRectangle.Y + Padding.Top,
               ActualClientRectangle.Width - Padding.Horizontal, ActualClientRectangle.Height - Padding.Vertical);

            var offset = 0;

            if (Orientation == Orientation.Vertical)
            {
                foreach (var child in Children)
                {
                    var childSize = child.CalculateRequiredClientSpace();

                    var positionX = child.Margin.Left;
                    var positionY = child.Margin.Top;
                    var height = childSize.Y;
                    var width = childSize.X;

                    switch (child.HorizontalAlignment)
                    {
                        case HorizontalAlignment.Right:
                            positionX = contentArea.Width - width - child.Margin.Right;
                            break;
                        case HorizontalAlignment.Left:
                            positionX = 0 + child.Margin.Left;
                            break;
                        case HorizontalAlignment.Stretch:
                            positionX = 0 + child.Margin.Left;
                            width = contentArea.Width - child.Margin.Horizontal;
                            break;
                        case HorizontalAlignment.Center:
                        default:
                            positionX = contentArea.Width / 2 - width / 2 - child.Margin.Horizontal / 2;
                            break;
                    }

                    child.ActualClientRectangle = new Rectangle(positionX, offset, width, height);

                    offset += height + child.Margin.Top;
                    offset += child.Margin.Bottom;
                    offset += ItemSpacing;

                }
            }
            else
            {
                foreach (var child in Children)
                {
                    var childSize = child.CalculateRequiredClientSpace();

                    var positionX = child.Margin.Left;
                    var positionY = child.Margin.Top;
                    var height = childSize.Y;
                    var width = childSize.X;

                    switch (child.VerticalAlignment)
                    {
                        case VerticalAlignment.Top:
                            positionY = 0 + child.Margin.Top;
                            break;
                        case VerticalAlignment.Bottom:
                            positionY = contentArea.Height - height - child.Margin.Bottom;
                            break;
                        case VerticalAlignment.Stretch:
                            positionY = 0 + child.Margin.Top;
                            height = contentArea.Height - child.Margin.Vertical;
                            break;
                        case VerticalAlignment.Center:
                        default:
                            positionY = contentArea.Height / 2 - height / 2 - child.Margin.Vertical / 2;
                            break;
                    }

                    child.ActualClientRectangle = new Rectangle(offset, positionY, width, height);
                    offset += width + child.Margin.Left;
                    offset += child.Margin.Right;
                    offset += ItemSpacing;
                }
            }
        }

        public override Point CalculateRequiredClientSpace()
        {
            var height = Height;
            var width = Width;

            var calcWidth = 0;
            var calcHeight = 0;

            if (height != null && width != null)
                return new Point(height ?? 0, width ?? 0);

            if(Orientation == Orientation.Vertical)
            {
                foreach(var child in Children)
                {
                    var childCalc = child.CalculateRequiredClientSpace();
                    calcHeight += childCalc.Y + child.Margin.Vertical;
                    if (childCalc.X + child.Margin.Horizontal > calcWidth)
                        calcWidth = childCalc.X + child.Margin.Horizontal;
                }

                calcHeight += (Children.Count - 1) * ItemSpacing;
            }
            else
            {
                foreach (var child in Children)
                {
                    var childCalc = child.CalculateRequiredClientSpace();
                    calcWidth += childCalc.X + child.Margin.Horizontal;
                    if (childCalc.Y + child.Margin.Vertical > calcHeight)
                        calcHeight = childCalc.Y + child.Margin.Vertical;
                }

                calcWidth += (Children.Count - 1) * ItemSpacing;
            }

            return new Point((width != null ? width ?? 0 : calcWidth), (height != null ? height ?? 0 : calcHeight));
        }
    }
}
