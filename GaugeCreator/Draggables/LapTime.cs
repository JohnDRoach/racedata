using System.Drawing;
using GaugeCreator.Draggables.Base;
using GaugeCreator.Data;

namespace GaugeCreator.Draggables
{
    public class LapTime : DraggableTextBase, IWidget
    {
        public LapTime()
        {
            SetText("Lap Time");
        }

        public void DrawYourselfOnThis(Graphics pane, DataHeader header, DataLine dataPoint)
        {
            DrawText(pane, string.Format("Lap Time: {0:0.00}", dataPoint.LapTime));
        }
    }
}
