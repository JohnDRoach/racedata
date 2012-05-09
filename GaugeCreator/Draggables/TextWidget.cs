using System;
using System.Drawing;
using System.Windows.Controls;

using GaugeCreator.Data;

namespace GaugeCreator.Draggables
{
    public class TextWidget : TextBlock, IWidget
    {
        private readonly IWidget attachedWidget;

        public TextWidget() : this(null)
        {
            
        }

        public TextWidget(IWidget attachedWidget)
        {
            this.attachedWidget = attachedWidget;

            Canvas.SetLeft(this, 0);
            Canvas.SetTop(this, 0);
        }

        public void DrawYourselfOnThis(Graphics pane, DataHeader header, DataLine dataPoint)
        {
            throw new NotImplementedException();
            //pane.DrawString("RPM: " + value, GaugeDisplay.DefaultFont, new SolidBrush(Color.FromArgb(102, 102, 102)), 0, 0);
        }
    }
}
