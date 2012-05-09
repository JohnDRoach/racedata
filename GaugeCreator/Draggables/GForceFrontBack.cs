using System.Drawing;
using GaugeCreator.Data;

namespace GaugeCreator.Draggables
{
    class GForceLeftRight : Base.DraggableBarBase, IWidget
    {
        public GForceLeftRight()
        {
            SetText("GForceLeftRight");
            MinValue = -1.5;
            MaxValue = 1.5;
        }

        public void DrawYourselfOnThis(Graphics pane, DataHeader header, DataLine dataPoint)
        {
            DrawBar(pane, dataPoint.GForcex, Color.Blue, string.Format("{0:0.00}", dataPoint.GForcex), Orientation.LeftToRight);
        }
    }
}
