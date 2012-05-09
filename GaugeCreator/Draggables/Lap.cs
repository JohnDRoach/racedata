using System.Drawing;
using GaugeCreator.Draggables.Base;
using GaugeCreator.Data;

namespace GaugeCreator.Draggables
{
    public class Lap : DraggableTextBase, IWidget
    {
        public Lap()
        {
            SetText("Lap");
        }

        public void DrawYourselfOnThis(Graphics pane, DataHeader header, DataLine dataPoint)
        {
            DrawText(pane, "Lap: " + dataPoint.Lap);
        }
    }
}
