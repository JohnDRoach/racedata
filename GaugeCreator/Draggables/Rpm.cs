using System.Drawing;
using GaugeCreator.Draggables.Base;
using GaugeCreator.Data;

namespace GaugeCreator.Draggables
{
    public class Rpm : DraggableTextBase, IWidget
    {
        public Rpm()
        {
            SetText("RPM");
        }

        public void DrawYourselfOnThis(Graphics pane, DataHeader header, DataLine dataPoint)
        {
            DrawText(pane, "RPM: " + dataPoint.Rpm);
        }
    }
}
