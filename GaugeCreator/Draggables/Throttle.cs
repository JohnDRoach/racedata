using System.Drawing;
using GaugeCreator.Draggables.Base;
using GaugeCreator.Data;

namespace GaugeCreator.Draggables
{
    class Throttle : DraggableBarBase, IWidget
    {
        public Throttle()
        {
            SetText("Throttle");
            MinValue = 0;
            MaxValue = 100;
        }

        public void DrawYourselfOnThis(Graphics pane, DataHeader header, DataLine dataPoint)
        {
            DrawBar(pane, dataPoint.Throttle, Color.Green, string.Format("{0:0.00}", dataPoint.Throttle), Orientation.BottomToTop);
        }
    }
}
