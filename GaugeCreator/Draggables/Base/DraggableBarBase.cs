using System;
using System.Drawing;

namespace GaugeCreator.Draggables.Base
{
    public abstract class DraggableBarBase : DraggableBase
    {
        public enum Orientation
        {
            LeftToRight,
            RightToLeft,
            TopToBottom,
            BottomToTop
        }

        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        private double Extent { get; set; }
        protected Color Colour { get; set; }

        protected void DrawBar(Graphics pane, double value, Color colour, string text, Orientation orientation)
        {
            Extent = Math.Abs(MaxValue - MinValue);
            Colour = colour;

            value = TransformValueWithinLimits(value);

            DrawZeroLine(orientation, pane);

            DrawBarLine(orientation, pane, value);

            DrawRectangle(pane);

            DrawText(text, orientation, pane);
        }
        
        private void DrawZeroLine(Orientation orientation, Graphics pane)
        {
            if (MinValue <= 0 && 0 <= MaxValue)
            {
                double multiplier;
                Point startPoint;
                Point endPoint;

                if (orientation == Orientation.LeftToRight || orientation == Orientation.TopToBottom)
                {
                    multiplier = (0.0 - MinValue)/Extent;
                }
                else
                {
                    multiplier = MaxValue / Extent;
                }

                if (orientation == Orientation.LeftToRight || orientation == Orientation.RightToLeft)
                {
                    int x = (int)(multiplier*GetWidth() + GetTopLeftX());
                    startPoint = new Point(x, (int)GetTopLeftY());
                    endPoint = new Point(x, (int)GetTopLeftY() + GetHeight());
                }
                else
                {
                    int y = (int)(multiplier * GetHeight() + GetTopLeftY());
                    startPoint = new Point((int)GetTopLeftX(), y);
                    endPoint = new Point((int)GetTopLeftX() + GetWidth(), y);
                }

                pane.DrawLine(new Pen(Colour, 1), startPoint, endPoint);
            }
        }

        private void DrawBarLine(Orientation orientation, Graphics pane, double value)
        {
            double multiplier;
            Point startPoint;
            Point endPoint;

            if (orientation == Orientation.LeftToRight || orientation == Orientation.TopToBottom)
            {
                multiplier = (0.0 - MinValue) / Extent;
            }
            else
            {
                multiplier = MaxValue / Extent;
            }

            if (orientation == Orientation.LeftToRight || orientation == Orientation.RightToLeft)
            {
                int x = (int)(multiplier * GetWidth() + GetTopLeftX());
                startPoint = new Point(x, (int)GetTopLeftY() + GetHeight() / 2);
                endPoint = new Point(x + (int)(value / Extent * GetWidth()), (int)GetTopLeftY() + GetHeight() / 2);
                pane.DrawLine(new Pen(Colour, GetHeight()), startPoint, endPoint);
            }
            else
            {
                int y = (int)(multiplier * GetHeight() + GetTopLeftY());
                startPoint = new Point((int)GetTopLeftX() + GetWidth() / 2, y);
                endPoint = new Point((int)GetTopLeftX() + GetWidth() / 2, y - (int)(value / Extent * GetHeight()));
                pane.DrawLine(new Pen(Colour, GetWidth()), startPoint, endPoint);
            }
        }

        private void DrawRectangle(Graphics pane)
        {
            pane.DrawRectangle(new Pen(Color.Black, 1), GetTopLeftX(), GetTopLeftY(), GetWidth(), GetHeight());
        }

        private void DrawText(string text, Orientation orientation, Graphics pane)
        {
            if (!string.IsNullOrEmpty(text))
            { 
                // if lefttoright will draw on the left of the centre line
                pane.DrawString(text, new Font(System.Drawing.FontFamily.GenericSerif, 10), Brushes.Black, GetTopLeftX(), GetTopLeftY());
                // if righttotleft will draw on the right of the centre line
                // if toptobottom will draw on the botton of the centre line
                // if bottomtottop will draw on the top of the centre line
            }
        }

        private double TransformValueWithinLimits(double value)
        {
            if (value > MaxValue)
            {
                return MaxValue;
            }

            if (value < MinValue)
            {
                return MinValue;
            }

            return value;
        }
    }
}
