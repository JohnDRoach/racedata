using System.Drawing;
using GaugeCreator.Draggables.Base;
using GaugeCreator.Data;

namespace GaugeCreator.Draggables
{
    public class ElapsedTime : DraggableTextBase, IWidget
    {
        public ElapsedTime()
        {
            SetText("Elapsed Time");
        }

        public void DrawYourselfOnThis(Graphics pane, DataHeader header, DataLine dataPoint)
        {
            DrawText(pane, string.Format("Time: {0:0.00}", dataPoint.Time));
        }
    }
}
