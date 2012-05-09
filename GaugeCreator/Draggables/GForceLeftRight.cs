using System.Drawing;
using GaugeCreator.Data;

namespace GaugeCreator.Draggables
{
    class GForceFrontBack : Base.DraggableBarBase, IWidget
    {
        public GForceFrontBack()
        {
            SetText("GForceFrontBack");
            MinValue = -1.5;
            MaxValue = 1.5;
        }

        public void DrawYourselfOnThis(Graphics pane, DataHeader header, DataLine dataPoint)
        {
            DrawBar(pane, dataPoint.GForcey, Color.Blue, string.Format("{0:0.00}", dataPoint.GForcey), Orientation.TopToBottom);
        }
    }
}
